using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class ChangePasswordRequest
    {
        public string UserEmail { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                List<User> users = await _userServices.GetUserByEmail(request.UserEmail);

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

                    if (user.UserPassword == request.OldPassword)
                    {
                        await _userServices.UpdatePassword(user, request.NewPassword);

                        return Ok(new
                        {
                            sucess = "Your password has been successfully changed !"
                        });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            error = "Incorrect old password !"
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