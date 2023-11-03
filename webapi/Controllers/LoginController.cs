using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;


namespace webapi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly UserServices _userServices;

        public LoginController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EFb2Mg83oilBJ6CELtgZpsW8878D8x7qwX+7A18lZo7eCQnvjlak+R/PgI3lioeT"));

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
    }
}
