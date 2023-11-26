﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/examination")]
    public class ExaminationController : ControllerBase
    {
        private readonly ExaminationServices _examinationServices;
        private readonly UserServices _userServices;

        public ExaminationController(ExaminationServices examinationServices, UserServices userServices)
        {
            _examinationServices = examinationServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllExaminations")]
        public async Task<IActionResult> GetAllExaminations()
        {
            try
            {
                List<Examination> examinations = await _examinationServices.GetAllExaminations();
                return Ok(examinations);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getExaminationById/{id}")]
        public async Task<IActionResult> GetExaminationById(string id)
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

                List<Examination> examinations = await _examinationServices.GetExaminationById(id);

                if (examinations.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No examination found !"
                    });
                }
                else
                    return Ok(examinations[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createExamination")]
        [Authorize]
        public async Task<IActionResult> CreateExamination([FromBody] Examination examination)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(examination.UserId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid user ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examination.LicenseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license ID !"
                        });
                    }

                    Examination e = new Examination
                    {
                        UserId = examination.UserId,
                        LicenseId = examination.LicenseId,
                        ExaminationDate = examination.ExaminationDate,
                        Score = examination.Score,
                        IsPassed = examination.IsPassed
                    };

                    await _examinationServices.CreateExamination(e);

                    return Ok(new
                    {
                        success = "Create examination successfully !"
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
        [Route("updateExamination/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateExamination(string id, [FromBody] Examination examination)
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

                    List<Examination> examinations = await _examinationServices.GetExaminationById(id);

                    if (examinations.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination found !"
                        });
                    }

                    if (!ObjectId.TryParse(examination.UserId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid user ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examination.LicenseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license ID !"
                        });
                    }

                    await _examinationServices.UpdateExamination(id, examination);

                    return Ok(new
                    {
                        success = "Update examination successfully !"
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
        [Route("deleteExamination/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExamination(string id)
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

                    List<Examination> examinations = await _examinationServices.GetExaminationById(id);

                    if (examinations.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination found !"
                        });
                    }

                    await _examinationServices.DeleteExamination(id);

                    return Ok(new
                    {
                        success = "Delete examination successfully !"
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