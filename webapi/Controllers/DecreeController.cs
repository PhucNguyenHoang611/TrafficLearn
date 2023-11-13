using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/decree")]
    public class DecreeController : ControllerBase
    {
        private readonly DecreeServices _decreeServices;
        private readonly UserServices _userServices;

        public DecreeController(DecreeServices decreeServices, UserServices userServices)
        {
            _decreeServices = decreeServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllDecrees")]
        public async Task<IActionResult> GetAllDecrees()
        {
            try
            {
                List<Decree> decrees = await _decreeServices.GetAllDecrees();
                return Ok(decrees);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getDecreeById/{id}")]
        public async Task<IActionResult> GetDecreeById(string id)
        {
            try
            {
                List<Decree> decrees = await _decreeServices.GetDecreeById(id);

                if (decrees.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No Decree found !"
                    });
                }
                else
                    return Ok(decrees[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createDecree")]
        [Authorize]
        public async Task<IActionResult> CreateDecree([FromBody] Decree decree)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    Decree d = new Decree
                    {
                        DecreeName = decree.DecreeName,
                        DecreeDate = decree.DecreeDate,
                        DecreeNumber = decree.DecreeNumber
                    };

                    await _decreeServices.CreateDecree(d);

                    return Ok(new
                    {
                        success = "Create decree successfully !"
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
        [Route("updateDecree/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDecree(string id, [FromBody] Decree decree)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    await _decreeServices.UpdateDecree(id, decree);

                    return Ok(new
                    {
                        success = "Update decree successfully !"
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
        [Route("deleteDecree/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDecree(string id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    await _decreeServices.DeleteDecree(id);

                    return Ok(new
                    {
                        success = "Delete decree successfully !"
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
    }
}