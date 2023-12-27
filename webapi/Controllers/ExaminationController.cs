using Microsoft.AspNetCore.Authorization;
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
        private readonly QuestionServices _questionServices;
        private readonly LicenseTitleServices _licenseTitleServices;
        private readonly LicenseServices _licenseServices;
        private readonly TitleServices _titleServices;
        private readonly UserServices _userServices;

        public ExaminationController(ExaminationServices examinationServices, QuestionServices questionServices, LicenseTitleServices licenseTitleServices, LicenseServices licenseServices, TitleServices titleServices, UserServices userServices)
        {
            _examinationServices = examinationServices;
            _questionServices = questionServices;
            _licenseTitleServices = licenseTitleServices;
            _licenseServices = licenseServices;
            _titleServices = titleServices;
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

        [HttpGet]
        [Route("getAllExaminationQuestions/{id}")]
        public async Task<IActionResult> GetAllExaminationQuestions(string id)
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

                List<QuestionDetails> result = new List<QuestionDetails>();
                List<Question> questions = await _examinationServices.GetAllQuestions(id);
                int numOfImportantQuestions = 0;

                if (questions.Count > 0)
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        if (questions[i].Important) numOfImportantQuestions++;

                        QuestionDetails qd = new QuestionDetails();
                        qd.Question = questions[i];

                        qd.Answers = new List<AnswerDetails>();
                        List<Answer> aList = await _questionServices.GetAllAnswers(questions[i].Id);

                        for (int j = 0; j < aList.Count; j++)
                        {
                            AnswerDetails ad = new AnswerDetails();
                            ad.AnswerId = aList[j].Id;
                            ad.Content = aList[j].AnswerContent;

                            qd.Answers.Add(ad);
                        }

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
                    if (!ObjectId.TryParse(examination.LicenseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license ID !"
                        });
                    }

                    Examination e = new Examination
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        LicenseId = examination.LicenseId,
                        ExaminationName = examination.ExaminationName
                    };

                    await _examinationServices.CreateExamination(e);

                    return Ok(new
                    {
                        success = "Create examination successfully !",
                        examinationId = e.Id
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