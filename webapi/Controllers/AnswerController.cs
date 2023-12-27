using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/answer")]
    public class AnswerController : ControllerBase
    {
        private readonly AnswerServices _answerServices;
        private readonly UserServices _userServices;

        public AnswerController(AnswerServices answerServices, UserServices userServices)
        {
            _answerServices = answerServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllAnswers")]
        [Authorize]
        public async Task<IActionResult> GetAllAnswers()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    List<Answer> answers = await _answerServices.GetAllAnswers();
                    return Ok(answers);
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
        [Route("getAnswerById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAnswerById(string id)
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

                    List<Answer> answers = await _answerServices.GetAnswerById(id);

                    if (answers.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No answer found !"
                        });
                    }
                    else
                        return Ok(answers[0]);
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
        [Route("validateAnswer/{questionId}/{answerId}")]
        [Authorize]
        public async Task<IActionResult> ValidateAnswer(string questionId, string answerId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "User")
                {
                    if (!ObjectId.TryParse(questionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    if (!ObjectId.TryParse(answerId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid answer ID !"
                        });
                    }

                    bool checkAnswer = await _answerServices.ValidateAnswer(questionId, answerId);

                    return Ok(new
                    {
                        result = checkAnswer
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

        [HttpPost]
        [Route("createAnswer")]
        [Authorize]
        public async Task<IActionResult> CreateAnswer([FromBody] Answer answer)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(answer.QuestionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    Answer a = new Answer
                    {
                        QuestionId = answer.QuestionId,
                        AnswerContent = answer.AnswerContent,
                        Result = answer.Result
                    };

                    await _answerServices.CreateAnswer(a);

                    return Ok(new
                    {
                        success = "Create answer successfully !"
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
        [Route("updateAnswer/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAnswer(string id, [FromBody] Answer answer)
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

                    List<Answer> answers = await _answerServices.GetAnswerById(id);

                    if (answers.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No answer found !"
                        });
                    }

                    if (!ObjectId.TryParse(answer.QuestionId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid question ID !"
                        });
                    }

                    await _answerServices.UpdateAnswer(id, answer);

                    return Ok(new
                    {
                        success = "Update answer successfully !"
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
        [Route("deleteAnswer/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAnswer(string id)
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

                    List<Answer> answers = await _answerServices.GetAnswerById(id);

                    if (answers.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No answer found !"
                        });
                    }

                    await _answerServices.DeleteAnswer(id);

                    return Ok(new
                    {
                        success = "Delete answer successfully !"
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