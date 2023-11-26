using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/trafficSignType")]
    public class TrafficSignTypeController : ControllerBase
    {
        private readonly TrafficSignTypeServices _trafficSignTypeServices;
        private readonly UserServices _userServices;

        public TrafficSignTypeController(TrafficSignTypeServices trafficSignTypeServices, UserServices userServices)
        {
            _trafficSignTypeServices = trafficSignTypeServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllTrafficSignTypes")]
        public async Task<IActionResult> GetAllTrafficSignTypes()
        {
            try
            {
                List<TrafficSignType> trafficSignTypes = await _trafficSignTypeServices.GetAllTrafficSignTypes();
                return Ok(trafficSignTypes);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getTrafficSignTypeById/{id}")]
        public async Task<IActionResult> GetTrafficSignTypeById(string id)
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

                List<TrafficSignType> trafficSignTypes = await _trafficSignTypeServices.GetTrafficSignTypeById(id);

                if (trafficSignTypes.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No type found !"
                    });
                }
                else
                    return Ok(trafficSignTypes[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createTrafficSignType")]
        [Authorize]
        public async Task<IActionResult> CreateTrafficSignType([FromBody] TrafficSignType trafficSignType)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    TrafficSignType tst = new TrafficSignType
                    {
                        SignType = trafficSignType.SignType
                    };

                    await _trafficSignTypeServices.CreateTrafficSignType(tst);

                    return Ok(new
                    {
                        success = "Create type successfully !"
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
        [Route("updateTrafficSignType/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTrafficSignType(string id, [FromBody] TrafficSignType trafficSignType)
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

                    List<TrafficSignType> trafficSignTypes = await _trafficSignTypeServices.GetTrafficSignTypeById(id);

                    if (trafficSignTypes.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No type found !"
                        });
                    }

                    await _trafficSignTypeServices.UpdateTrafficSignType(id, trafficSignType);

                    return Ok(new
                    {
                        success = "Update type successfully !"
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
        [Route("deleteTrafficSignType/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrafficSignType(string id)
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

                    List<TrafficSignType> trafficSignTypes = await _trafficSignTypeServices.GetTrafficSignTypeById(id);

                    if (trafficSignTypes.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No type found !"
                        });
                    }

                    await _trafficSignTypeServices.DeleteTrafficSignType(id);

                    return Ok(new
                    {
                        success = "Delete type successfully !"
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