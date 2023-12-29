using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/examinationQuestion")]
    public class ExaminationQuestionController : ControllerBase
    {
        private readonly ExaminationQuestionServices _examinationQuestionServices;
        private readonly UserServices _userServices;

        public ExaminationQuestionController(ExaminationQuestionServices examinationQuestionServices, UserServices userServices)
        {
            _examinationQuestionServices = examinationQuestionServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllExaminationQuestions")]
        public async Task<IActionResult> GetAllExaminationQuestions()
        {
            try
            {
                List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetAllExaminationQuestions();
                return Ok(examinationQuestions);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getExaminationQuestionById/{id}")]
        public async Task<IActionResult> GetExaminationQuestionById(string id)
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

                List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetExaminationQuestionById(id);

                if (examinationQuestions.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No examination question found !"
                    });
                }
                else
                    return Ok(examinationQuestions[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getExaminationQuestion/{examinationId}/{questionId}")]
        [Authorize]
        public async Task<IActionResult> GetExaminationQuestion(string examinationId, string questionId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(examinationId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid examination ID !"
                        });
                    }

                    if (!ObjectId.TryParse(questionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetExaminationQuestion(examinationId, questionId);

                    if (examinationQuestions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination question found !"
                        });
                    }
                    else
                        return Ok(examinationQuestions);
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
        [Route("getAllExaminationQuestionsByExaminationId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllExaminationQuestionsByExaminationId(string id)
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
                            error = "Invalid examination ID !"
                        });
                    }

                    List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetAllExaminationQuestionsByExaminationId(id);

                    if (examinationQuestions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination question found !"
                        });
                    }
                    else
                        return Ok(examinationQuestions);
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
        [Route("getAllExaminationQuestionsByQuestionId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllExaminationQuestionsByQuestionId(string id)
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
                            error = "Invalid question ID !"
                        });
                    }

                    List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetAllExaminationQuestionsByQuestionId(id);

                    if (examinationQuestions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination question found !"
                        });
                    }
                    else
                        return Ok(examinationQuestions);
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
        [Route("createExaminationQuestion")]
        [Authorize]
        public async Task<IActionResult> CreateExaminationQuestion([FromBody] ExaminationQuestion examinationQuestion)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(examinationQuestion.ExaminationId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid examination ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationQuestion.QuestionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    ExaminationQuestion e = new ExaminationQuestion
                    {
                        ExaminationId = examinationQuestion.ExaminationId,
                        QuestionId = examinationQuestion.QuestionId
                    };

                    await _examinationQuestionServices.CreateExaminationQuestion(e);

                    return Ok(new
                    {
                        success = "Create examination question successfully !"
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
        [Route("updateExaminationQuestion/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateExaminationQuestion(string id, [FromBody] ExaminationQuestion examinationQuestion)
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

                    List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetExaminationQuestionById(id);

                    if (examinationQuestions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination question found !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationQuestion.ExaminationId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid examination ID !"
                        });
                    }

                    if (!ObjectId.TryParse(examinationQuestion.QuestionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    await _examinationQuestionServices.UpdateExaminationQuestion(id, examinationQuestion);

                    return Ok(new
                    {
                        success = "Update examination question successfully !"
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
        [Route("deleteExaminationQuestion/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExaminationQuestion(string id)
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

                    List<ExaminationQuestion> examinationQuestions = await _examinationQuestionServices.GetExaminationQuestionById(id);

                    if (examinationQuestions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No examination question found !"
                        });
                    }

                    await _examinationQuestionServices.DeleteExaminationQuestion(id);

                    return Ok(new
                    {
                        success = "Delete examination question successfully !"
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