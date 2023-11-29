using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/question")]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionServices _questionServices;
        private readonly FileServices _fileServices;
        private readonly UserServices _userServices;

        public QuestionController(QuestionServices questionServices, FileServices fileServices, UserServices userServices)
        {
            _questionServices = questionServices;
            _fileServices = fileServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                List<Question> questions = await _questionServices.GetAllQuestions();
                return Ok(questions);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getQuestionById/{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
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

                List<Question> questions = await _questionServices.GetQuestionById(id);

                if (questions.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No question found !"
                    });
                }
                else
                    return Ok(questions[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllAnswers/{id}")]
        public async Task<IActionResult> GetAllAnswers(string id)
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

                List<Answer> answers = await _questionServices.GetAllAnswers(id);

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
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createQuestion")]
        [Authorize]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    Question q = new Question
                    {
                        TestGroup = question.TestGroup,
                        QuestionType = question.QuestionType,
                        QuestionContent = question.QuestionContent,
                        Important = question.Important,
                        Explanation = question.Explanation
                    };

                    await _questionServices.CreateQuestion(q);

                    return Ok(new
                    {
                        success = "Create question successfully !"
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
        [Route("uploadQuestionMedia/{id}")]
        [Authorize]
        public async Task<IActionResult> UploadQuestionMedia(string id, IFormFile file)
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

                    List<Question> questions = await _questionServices.GetQuestionById(id);

                    if (questions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No question found !"
                        });
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            string fileUrl = await _fileServices.UploadToFirebaseStorage(memoryStream, "Question_" + file.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff"));

                            questions[0].QuestionMedia = fileUrl;
                            await _questionServices.UpdateQuestion(id, questions[0]);

                            return Ok(new
                            {
                                success = "Upload question media successfully !"
                            });
                        }
                    }
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
        [Route("updateQuestion/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuestion(string id, [FromBody] Question question)
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

                    List<Question> questions = await _questionServices.GetQuestionById(id);

                    if (questions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No question found !"
                        });
                    }

                    await _questionServices.UpdateQuestion(id, question);

                    return Ok(new
                    {
                        success = "Update question successfully !"
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
        [Route("deleteQuestion/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(string id)
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

                    List<Question> questions = await _questionServices.GetQuestionById(id);

                    if (questions.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No question found !"
                        });
                    }

                    await _questionServices.DeleteQuestion(id);

                    return Ok(new
                    {
                        success = "Delete question successfully !"
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