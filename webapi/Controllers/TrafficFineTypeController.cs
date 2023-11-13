using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/trafficFineType")]
    public class TrafficFineTypeController : ControllerBase
    {
        private readonly TrafficFineTypeServices _trafficFineTypeServices;
        private readonly UserServices _userServices;

        public TrafficFineTypeController(TrafficFineTypeServices trafficFineTypeServices, UserServices userServices)
        {
            _trafficFineTypeServices = trafficFineTypeServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllTrafficFineTypes")]
        public async Task<IActionResult> GetAllTrafficFineTypes()
        {
            try
            {
                List<TrafficFineType> trafficFineTypes = await _trafficFineTypeServices.GetAllTrafficFineTypes();
                return Ok(trafficFineTypes);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getTrafficFineTypeById/{id}")]
        public async Task<IActionResult> GetTrafficFineTypeById(string id)
        {
            try
            {
                List<TrafficFineType> trafficFineTypes = await _trafficFineTypeServices.GetTrafficFineTypeById(id);

                if (trafficFineTypes.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No type found !"
                    });
                }
                else
                    return Ok(trafficFineTypes[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createTrafficFineType")]
        [Authorize]
        public async Task<IActionResult> CreateTrafficFineType([FromBody] TrafficFineType trafficFineType)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    TrafficFineType tft = new TrafficFineType
                    {
                        FineType = trafficFineType.FineType
                    };

                    await _trafficFineTypeServices.CreateTrafficFineType(tft);

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
        [Route("updateTrafficFineType/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTrafficFineType(string id, [FromBody] TrafficFineType trafficFineType)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    await _trafficFineTypeServices.UpdateTrafficFineType(id, trafficFineType);

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
        [Route("deleteTrafficFineType/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrafficFineType(string id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    await _trafficFineTypeServices.DeleteTrafficFineType(id);

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