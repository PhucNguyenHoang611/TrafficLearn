using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Drawing;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/trafficFine")]
    public class TrafficFineController : ControllerBase
    {
        private readonly TrafficFineServices _trafficFineServices;
        private readonly UserServices _userServices;

        public TrafficFineController(TrafficFineServices trafficFineServices, UserServices userServices)
        {
            _trafficFineServices = trafficFineServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllTrafficFines")]
        public async Task<IActionResult> GetAllTrafficFines()
        {
            try
            {
                List<TrafficFine> trafficFines = await _trafficFineServices.GetAllTrafficFines();
                return Ok(trafficFines);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getTrafficFineById/{id}")]
        public async Task<IActionResult> GetTrafficFineById(string id)
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

                List<TrafficFine> trafficFines = await _trafficFineServices.GetTrafficFineById(id);

                if (trafficFines.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No traffic fine found !"
                    });
                }
                else
                    return Ok(trafficFines[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createTrafficFine")]
        [Authorize]
        public async Task<IActionResult> CreateTrafficFine([FromBody] TrafficFine trafficFine)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(trafficFine.FineTypeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid type ID !"
                        });
                    }

                    TrafficFine tf = new TrafficFine
                    {
                        FineName = trafficFine.FineName,
                        FineTypeId = trafficFine.FineTypeId,
                        VehicleType = trafficFine.VehicleType,
                        FineBehavior = trafficFine.FineBehavior,
                        FineContent = trafficFine.FineContent,
                        FineAdditional = trafficFine.FineAdditional,
                        FineNote = trafficFine.FineNote
                    };

                    await _trafficFineServices.CreateTrafficFine(tf);

                    return Ok(new
                    {
                        success = "Create traffic fine successfully !"
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
        [Route("updateTrafficFine/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTrafficFine(string id, [FromBody] TrafficFine trafficFine)
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

                    List<TrafficFine> trafficFines = await _trafficFineServices.GetTrafficFineById(id);

                    if (trafficFines.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No traffic fine found !"
                        });
                    }

                    if (!ObjectId.TryParse(trafficFine.FineTypeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid type ID !"
                        });
                    }

                    await _trafficFineServices.UpdateTrafficFine(id, trafficFine);

                    return Ok(new
                    {
                        success = "Update traffic fine successfully !"
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
        [Route("deleteTrafficFine/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrafficFine(string id)
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

                    List<TrafficFine> trafficFines = await _trafficFineServices.GetTrafficFineById(id);

                    if (trafficFines.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No traffic fine found !"
                        });
                    }

                    await _trafficFineServices.DeleteTrafficFine(id);

                    return Ok(new
                    {
                        success = "Delete traffic fine successfully !"
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