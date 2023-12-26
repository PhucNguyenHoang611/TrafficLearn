using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Drawing;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class QuestionDetails
    {
        public Question Question { get; set; } = null!;
        public License License { get; set; } = null!;
        public Title Title { get; set; } = null!;
    }

    [ApiController]
    [Route("api/question")]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionServices _questionServices;
        private readonly LicenseTitleServices _licenseTitleServices;
        private readonly LicenseServices _licenseServices;
        private readonly TitleServices _titleServices;

        private readonly FileServices _fileServices;
        private readonly UserServices _userServices;

        public QuestionController(QuestionServices questionServices, LicenseTitleServices licenseTitleServices, LicenseServices licenseServices, TitleServices titleServices, FileServices fileServices, UserServices userServices)
        {
            _questionServices = questionServices;
            _licenseTitleServices = licenseTitleServices;
            _licenseServices = licenseServices;
            _titleServices = titleServices;

            _fileServices = fileServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                List<QuestionDetails> result = new List<QuestionDetails>();
                List<Question> questions = await _questionServices.GetAllQuestions();

                if (questions.Count > 0)
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        QuestionDetails qd = new QuestionDetails();
                        qd.Question = questions[i];

                        List<LicenseTitle> ltList = await _licenseTitleServices.GetLicenseTitleById(questions[i].LicenseTitleId);
                        LicenseTitle lt = ltList[0];

                        List<License> lList = await _licenseServices.GetLicenseById(lt.LicenseId);
                        qd.License = lList[0];

                        List<Title> tList = await _titleServices.GetTitleById(lt.TitleId);
                        qd.Title = tList[0];

                        result.Add(qd);
                    }
                }

                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllQuestionsByLicenseId/{id}")]
        public async Task<IActionResult> GetAllQuestionsByLicenseId(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out _))
                {
                    return BadRequest(new
                    {
                        error = "Invalid license ID !"
                    });
                }

                List<QuestionDetails> result = new List<QuestionDetails>();
                List<Question> questions = await _questionServices.GetAllQuestions();
                int numOfImportantQuestions = 0;

                if (questions.Count > 0)
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        if (questions[i].Important) numOfImportantQuestions++;

                        QuestionDetails qd = new QuestionDetails();
                        qd.Question = questions[i];

                        List<LicenseTitle> ltList = await _licenseTitleServices.GetLicenseTitleById(questions[i].LicenseTitleId);
                        LicenseTitle lt = ltList[0];

                        List<License> lList = await _licenseServices.GetLicenseById(lt.LicenseId);
                        qd.License = lList[0];

                        List<Title> tList = await _titleServices.GetTitleById(lt.TitleId);
                        qd.Title = tList[0];

                        if (qd.License.Id == id) result.Add(qd);
                    }
                }

                return Ok(new
                {
                    data = result,
                    total = result.Count,
                    numberOfImportantQuestions = numOfImportantQuestions
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllImportantQuestionsByLicenseId/{id}")]
        public async Task<IActionResult> GetAllImportantQuestionsByLicenseId(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out _))
                {
                    return BadRequest(new
                    {
                        error = "Invalid license ID !"
                    });
                }

                List<QuestionDetails> result = new List<QuestionDetails>();
                List<Question> questions = await _questionServices.GetAllQuestions();

                if (questions.Count > 0)
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        QuestionDetails qd = new QuestionDetails();
                        qd.Question = questions[i];

                        List<LicenseTitle> ltList = await _licenseTitleServices.GetLicenseTitleById(questions[i].LicenseTitleId);
                        LicenseTitle lt = ltList[0];

                        List<License> lList = await _licenseServices.GetLicenseById(lt.LicenseId);
                        qd.License = lList[0];

                        List<Title> tList = await _titleServices.GetTitleById(lt.TitleId);
                        qd.Title = tList[0];

                        if (qd.License.Id == id && qd.Question.Important) result.Add(qd);
                    }
                }

                return Ok(new
                {
                    data = result,
                    total = result.Count
                });
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
                {
                    QuestionDetails qd = new QuestionDetails();
                    qd.Question = questions[0];

                    List<LicenseTitle> ltList = await _licenseTitleServices.GetLicenseTitleById(questions[0].LicenseTitleId);
                    LicenseTitle lt = ltList[0];

                    List<License> lList = await _licenseServices.GetLicenseById(lt.LicenseId);
                    qd.License = lList[0];

                    List<Title> tList = await _titleServices.GetTitleById(lt.TitleId);
                    qd.Title = tList[0];

                    return Ok(qd);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getQuestionsByLicenseTitleId/{id}")]
        public async Task<IActionResult> GetQuestionsByLicenseTitleId(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out _))
                {
                    return BadRequest(new
                    {
                        error = "Invalid license title ID !"
                    });
                }

                List<QuestionDetails> result = new List<QuestionDetails>();
                List<Question> questions = await _questionServices.GetQuestionsByLicenseTitleId(id);
                int numOfImportantQuestions = 0;

                if (questions.Count > 0)
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        if (questions[i].Important) numOfImportantQuestions++;

                        QuestionDetails qd = new QuestionDetails();
                        qd.Question = questions[i];

                        List<LicenseTitle> ltList = await _licenseTitleServices.GetLicenseTitleById(questions[i].LicenseTitleId);
                        LicenseTitle lt = ltList[0];

                        List<License> lList = await _licenseServices.GetLicenseById(lt.LicenseId);
                        qd.License = lList[0];

                        List<Title> tList = await _titleServices.GetTitleById(lt.TitleId);
                        qd.Title = tList[0];

                        result.Add(qd);
                    }
                }

                return Ok(new
                {
                    data = result,
                    total = result.Count,
                    numberOfImportantQuestions = numOfImportantQuestions
                });
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
                    if (!ObjectId.TryParse(question.LicenseTitleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license title ID !"
                        });
                    }

                    Question q = new Question
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        LicenseTitleId = question.LicenseTitleId,
                        QuestionContent = question.QuestionContent,
                        Important = question.Important,
                        Explanation = question.Explanation
                    };

                    await _questionServices.CreateQuestion(q);

                    return Ok(new
                    {
                        success = "Create question successfully !",
                        questionId = q.Id
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
                                success = "Upload question media successfully !",
                                mediaURL = fileUrl
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

                    if (!ObjectId.TryParse(question.LicenseTitleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license title ID !"
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