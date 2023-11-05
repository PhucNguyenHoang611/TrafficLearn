﻿using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;

namespace webapi.Controllers
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

        public RegisterController(UserServices userServices, EmailServices emailServices)
        {
            _userServices = userServices;
            _emailServices = emailServices;
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

            User user = new User
            {
                UserEmail = request.Email,
                UserPassword = request.Password,
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
        [Route("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            try
            {
                var check = await _userServices.ValidateTOTP(request.Email, request.TOTP);
                if (check)
                {
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
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("checkValid")]
        public async Task<Boolean> CheckValid([FromQuery] String email)
        {
            var res = await _userServices.GetUserByEmail(email) as dynamic;
            return (res.Count > 0) && res[0].IsVerified && res[0].IsActive;
        }

        [HttpPost]
        [Route("sendVerificationEmail")]
        public async Task<IActionResult> SendVerificationEmail([FromQuery] String email)
        {
            try
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
            catch
            {
                throw;
            }
        }
    }
}