using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Authentication;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly IConfiguration _configuration;

        public LoginController(UserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Email)
            };

            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TrafficLearnBackend",
                audience: "TrafficLearnBackend",
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            List<User> users = _userServices.GetUserByEmail(request.Email).Result;

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

        [HttpGet]
        [Route("google/callback")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = _configuration["Authentication:Google:RedirectUri"],
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
                userId = userId,
                userEmail = userEmail,
                userFirstName = userFirstName,
                userLastName = userLastName
            });
        }
    }
}
