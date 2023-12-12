using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using webapi.Models.Request;
using webapi.Services.Email;
using webapi.Services.PasswordHasher;

namespace webapi.Controllers.Authentication
{
    public class VerifyEmailRequest
    {
        public string Email { get; set; } = string.Empty;
        public int TOTP { get; set; }
    }

    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly EmailServices _emailServices;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterController(UserServices userServices, EmailServices emailServices, IPasswordHasher passwordHasher)
        {
            _userServices = userServices;
            _emailServices = emailServices;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            List<User> users = _userServices.GetUserByEmail(request.Email).Result;

            if (users.Count > 0)
            {
                return BadRequest(new
                {
                    error = "Email address is already in use !"
                });
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            User user = new User
            {
                UserEmail = request.Email,
                UserPassword = passwordHash,
                UserFirstName = request.FirstName,
                UserLastName = request.LastName,
                UserBirthday = request.Birthday,
                UserGender = request.Gender,
                UserPhoneNumber = request.PhoneNumber,
                UserProvider = request.Provider,
                UserRole = request.Role
            };

            await _userServices.CreateUser(user);
            int totp = await _userServices.GetTOTP(user.UserEmail);

            EmailRequest req = new EmailRequest
            {
                ToEmail = user.UserEmail,
                Subject = "Email Verification",
                Body = $"<p>Please input this OTP Code to finish your registration process: <b>{totp}</b></p>"
            };
            await _emailServices.SendEmailAsync(req);

            return Ok(new
            {
                success = "Your account has been successfully created !"
            });
        }

        [HttpPost]
        [Route("sendVerificationEmail/{email}")]
        public async Task<IActionResult> SendVerificationEmail(string email)
        {
            try
            {
                List<User> users = await _userServices.GetUserByEmail(email);

                if (users.Count == 0)
                {
                    return BadRequest(new
                    {
                        error = "Email doesn't exist !"
                    });
                }
                else
                {
                    int totp = await _userServices.GetTOTP(email);

                    EmailRequest request = new EmailRequest
                    {
                        ToEmail = email,
                        Subject = "Email Verification",
                        Body = $"<p>Please input this OTP Code to finish your registration process: <b>{totp}</b></p>"
                    };

                    await _emailServices.SendEmailAsync(request);

                    return Ok(new
                    {
                        success = "Verification code has been sent to your email !"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            try
            {
                List<User> users = await _userServices.GetUserByEmail(request.Email);

                if (users.Count == 0)
                {
                    return BadRequest(new
                    {
                        error = "Email doesn't exist !"
                    });
                }
                else
                {
                    User user = users[0];
                    var check = _userServices.ValidateTOTP(user, request.TOTP);

                    if (check)
                    {
                        await _userServices.VerifyUser(user);

                        return Ok(new
                        {
                            success = "Your account has been successfully verified !"
                        });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            error = "Invalid OTP !"
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}