using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Authentication;
using webapi.Models.Request;
using webapi.Services.Email;
using Azure.Security.KeyVault.Secrets;

namespace webapi.Controllers.Authentication
{
    public class ResetPasswordBody
    {
        public string Email { get; set; } = string.Empty;
        public int TOTP { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly EmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public LoginController(UserServices userServices, EmailServices emailServices, IConfiguration configuration, SecretClient secretClient)
        {
            _userServices = userServices;
            _emailServices = emailServices;
            _configuration = configuration;
            _secretClient = secretClient;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
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

                if (request.Password != user.UserPassword)
                {
                    return BadRequest(new
                    {
                        error = "Incorrect password !"
                    });
                }

                if (!user.IsVerified)
                {
                    return BadRequest(new
                    {
                        error = "Please verify your email !"
                    });
                }
                else if (!user.IsActive)
                {
                    return BadRequest(new
                    {
                        error = "Your email isn't active anymore !"
                    });
                }
                else
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserEmail)
                    };

                    /*var secretKey = _configuration["Jwt:SecretKey"];*/
                    var secretKey = _secretClient.GetSecret("Jwt-SecretKey").Value.Value.ToString();
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "TrafficLearnBackend",
                        audience: "TrafficLearnBackend",
                        claims: claims,
                        expires: DateTime.Now.AddDays(5),
                        signingCredentials: credentials
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        id = user.Id,
                        email = user.UserEmail,
                        firstName = user.UserFirstName,
                        lastName = user.UserLastName,
                        birthday = user.UserBirthday,
                        gender = user.UserGender,
                        phoneNumber = user.UserPhoneNumber,
                        provider = user.UserProvider,
                        isAdmin = user.UserRole == "Admin" ? true : false
                    });
                }
            }
        }

        [HttpPost]
        [Route("forgetPassword/{email}")]
        public async Task<IActionResult> ForgetPassword(string email)
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
                        Subject = "Reset Your Password",
                        Body = $"<h4>We received a request to reset the password for your account</h4><br>" +
                            $"<p>To reset your password, use this OTP Code: <b>{totp}</b></p>"
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
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordBody body)
        {
            try
            {
                List<User> users = await _userServices.GetUserByEmail(body.Email);

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
                    var check = _userServices.ValidateTOTP(user, body.TOTP);

                    if (check)
                    {
                        await _userServices.UpdatePassword(user, body.NewPassword);
                        return Ok(new
                        {
                            success = "Reset password successfully !"
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

        [HttpGet]
        [Route("google/callback")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties()
            {
                /*RedirectUri = _configuration["Authentication:Google:RedirectUri"],*/
                RedirectUri = _secretClient.GetSecret("Authentication-Google-RedirectUri").Value.Value.ToString(),
                AllowRefresh = true
            };

            return Challenge(properties, "TrafficLearn");
        }

        [HttpGet]
        [Route("google/success")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GoogleResponse()
        {
            var userInfo = await HttpContext.AuthenticateAsync("TrafficLearn");

            var userId = userInfo.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = userInfo.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var userFirstName = userInfo.Principal.FindFirst(ClaimTypes.GivenName)?.Value;
            var userLastName = userInfo.Principal.FindFirst(ClaimTypes.Surname)?.Value;

            return Ok(new
            {
                userId,
                userEmail,
                userFirstName,
                userLastName
            });
        }
    }
}