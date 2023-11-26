﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/trafficSign")]
    public class TrafficSignController : ControllerBase
    {
        private readonly TrafficSignServices _trafficSignServices;
        private readonly UserServices _userServices;

        public TrafficSignController(TrafficSignServices trafficSignServices, UserServices userServices)
        {
            _trafficSignServices = trafficSignServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllTrafficSigns")]
        public async Task<IActionResult> GetAllTrafficSigns()
        {
            try
            {
                List<TrafficSign> trafficSigns = await _trafficSignServices.GetAllTrafficSigns();
                return Ok(trafficSigns);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getTrafficSignById/{id}")]
        public async Task<IActionResult> GetTrafficSignById(string id)
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

                List<TrafficSign> trafficSigns = await _trafficSignServices.GetTrafficSignById(id);

                if (trafficSigns.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No traffic sign found !"
                    });
                }
                else
                    return Ok(trafficSigns[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createTrafficSign")]
        [Authorize]
        public async Task<IActionResult> CreateTrafficSign([FromBody] TrafficSign trafficSign)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(trafficSign.SignTypeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid type ID !"
                        });
                    }

                    TrafficSign ts = new TrafficSign
                    {
                        SignName = trafficSign.SignName,
                        SignTypeId = trafficSign.SignTypeId,
                        SignImage = trafficSign.SignImage,
                        SignExplanation = trafficSign.SignExplanation
                    };

                    await _trafficSignServices.CreateTrafficSign(ts);

                    return Ok(new
                    {
                        success = "Create traffic sign successfully !"
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
        [Route("updateTrafficSign/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTrafficSign(string id, [FromBody] TrafficSign trafficSign)
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

                    List<TrafficSign> trafficSigns = await _trafficSignServices.GetTrafficSignById(id);

                    if (trafficSigns.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No traffic sign found !"
                        });
                    }

                    if (!ObjectId.TryParse(trafficSign.SignTypeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid type ID !"
                        });
                    }

                    await _trafficSignServices.UpdateTrafficSign(id, trafficSign);

                    return Ok(new
                    {
                        success = "Update traffic sign successfully !"
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
        [Route("deleteTrafficSign/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrafficSign(string id)
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

                    List<TrafficSign> trafficSigns = await _trafficSignServices.GetTrafficSignById(id);

                    if (trafficSigns.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No traffic sign found !"
                        });
                    }

                    await _trafficSignServices.DeleteTrafficSign(id);

                    return Ok(new
                    {
                        success = "Delete traffic sign successfully !"
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