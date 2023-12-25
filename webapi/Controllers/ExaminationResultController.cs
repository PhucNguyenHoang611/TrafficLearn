using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/examinationResult")]
    public class ExaminationResultController : ControllerBase
    {
        private readonly ExaminationResultServices _examinationResultServices;
        private readonly UserServices _userServices;

        public ExaminationResultController(ExaminationResultServices examinationResultServices, UserServices userServices)
        {
            _examinationResultServices = examinationResultServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllExaminationResults/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllExaminations(string userId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "User")
                {
                    if (!ObjectId.TryParse(userId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid user ID !"
                        });
                    }

                    List<ExaminationResult> examinationResults = await _examinationResultServices.GetAllExaminationResults(userId);
                    return Ok(examinationResults);
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
        [Route("getExaminationResultById/{id}")]
        public async Task<IActionResult> GetExaminationResultById(string id)
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

                List<ExaminationResult> examinationResults = await _examinationResultServices.GetExaminationResultById(id);

                if (examinationResults.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No examination result found !"
                    });
                }
                else
                    return Ok(examinationResults[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createExaminationResult")]
        [Authorize]
        public async Task<IActionResult> CreateExaminationResult([FromBody] ExaminationResult examinationResult)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "User")
                {
                    if (!ObjectId.TryParse(examinationResult.UserId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid user ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationResult.ExaminationId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid examination ID !"
                        });
                    }

                    ExaminationResult er = new ExaminationResult
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        UserId = examinationResult.UserId,
                        ExaminationId = examinationResult.ExaminationId,
                        ExaminationDate = examinationResult.ExaminationDate,
                        Score = examinationResult.Score,
                        IsPassed = examinationResult.IsPassed
                    };

                    await _examinationResultServices.CreateExaminationResult(er);

                    return Ok(new
                    {
                        success = "Create examination result successfully !",
                        examinationResultId = er.Id
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
        [Route("updateExaminationResult/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateExaminationResult(string id, [FromBody] ExaminationResult examinationResult)
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

                    List<ExaminationResult> examinationResults = await _examinationResultServices.GetExaminationResultById(id);

                    if (examinationResults.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination result found !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationResult.UserId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid user ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationResult.ExaminationId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid examination ID !"
                        });
                    }

                    await _examinationResultServices.UpdateExaminationResult(id, examinationResult);

                    return Ok(new
                    {
                        success = "Update examination result successfully !"
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
        [Route("deleteExaminationResult/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExaminationResult(string id)
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

                    List<ExaminationResult> examinationResults = await _examinationResultServices.GetExaminationResultById(id);

                    if (examinationResults.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination result found !"
                        });
                    }

                    await _examinationResultServices.DeleteExaminationResult(id);

                    return Ok(new
                    {
                        success = "Delete examination result successfully !"
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