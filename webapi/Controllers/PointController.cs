using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/point")]
    public class PointController : ControllerBase
    {
        private readonly PointServices _pointServices;
        private readonly UserServices _userServices;

        public PointController(PointServices pointServices, UserServices userServices)
        {
            _pointServices = pointServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllPoints")]
        public async Task<IActionResult> GetAllPoints()
        {
            try
            {
                List<Point> points = await _pointServices.GetAllPoints();
                return Ok(points);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getPointsByClauseId/{id}")]
        public async Task<IActionResult> GetPointsByClauseId(string id)
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

                List<Point> points = await _pointServices.GetPointsByClauseId(id);
                return Ok(points);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getPointById/{id}")]
        public async Task<IActionResult> GetPointById(string id)
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

                List<Point> points = await _pointServices.GetPointById(id);

                if (points.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No point found !"
                    });
                }
                else
                    return Ok(points[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createPoint")]
        [Authorize]
        public async Task<IActionResult> CreatePoint([FromBody] Point point)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {

                    if (!ObjectId.TryParse(point.ClauseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid clause ID !"
                        });
                    }

                    Point p = new Point
                    {
                        ClauseId = point.ClauseId,
                        PointTitle = point.PointTitle,
                        PointContent = point.PointContent
                    };

                    await _pointServices.CreatePoint(p);

                    return Ok(new
                    {
                        success = "Create point successfully !"
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
        [Route("updatePoint/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePoint(string id, [FromBody] Point point)
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

                    List<Point> points = await _pointServices.GetPointById(id);

                    if (points.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No point found !"
                        });
                    }

                    if (!ObjectId.TryParse(point.ClauseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid clause ID !"
                        });
                    }

                    await _pointServices.UpdatePoint(id, point);

                    return Ok(new
                    {
                        success = "Update point successfully !"
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
        [Route("deletePoint/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePoint(string id)
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

                    List<Point> points = await _pointServices.GetPointById(id);

                    if (points.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No point found !"
                        });
                    }

                    await _pointServices.DeletePoint(id);

                    return Ok(new
                    {
                        success = "Delete point successfully !"
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