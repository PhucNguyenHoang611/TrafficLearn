using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    List<User> users = await _userServices.GetAllUsers();
                    return Ok(users);
                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Unauthorized user !"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out _))
                {
                    return BadRequest(new
                    {
                        error = "Invalid ID !"
                    });
                }

                List<User> users = await _userServices.GetUserById(id);

                if (users.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No user found !"
                    });
                }
                else
                    return Ok(users[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "User")
                {
                    if (!ObjectId.TryParse(id, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid ID !"
                        });
                    }

                    List<User> users = await _userServices.GetUserById(id);

                    if (users.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No user found !"
                        });
                    }

                    await _userServices.UpdateUser(id, user);

                    return Ok(new
                    {
                        success = "Update user successfully !"
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Unauthorized user !"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updateUserByAdmin/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserByAdmin(string id, [FromBody] User user)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(id, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid ID !"
                        });
                    }

                    List<User> users = await _userServices.GetUserById(id);

                    if (users.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No user found !"
                        });
                    }

                    await _userServices.UpdateUser(id, user);

                    return Ok(new
                    {
                        success = "Update user successfully !"
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Unauthorized user !"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(id, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid ID !"
                        });
                    }

                    List<User> users = await _userServices.GetUserById(id);

                    if (users.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No user found !"
                        });
                    }

                    await _userServices.DeleteUser(id);

                    return Ok(new
                    {
                        success = "Delete user successfully !"
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Unauthorized user !"
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("activeOrInactiveUser/{id}")]
        [Authorize]
        public async Task<IActionResult> ActiveOrInactiveUser(string id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(id, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid ID !"
                        });
                    }

                    List<User> users = await _userServices.GetUserById(id);

                    if (users.Count == 0)
                    {
                        return BadRequest(new
                        {
                            error = "No user found !"
                        });
                    }
                    else
                    {
                        User user = users[0];
                        user.IsActive = user.IsActive ? false : true;
                        await _userServices.UpdateUser(id, user);

                        return Ok(new
                        {
                            success = user.IsActive ? "Active user successfully !" : "Inactive user successfully !"
                        });
                    }
                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Unauthorized user !"
                    });
                }
            }
            catch
            {
                throw;
            }
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