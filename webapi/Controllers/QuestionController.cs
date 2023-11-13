using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserServices _userServices;

        public QuestionController(QuestionServices questionServices, UserServices userServices)
        {
            _questionServices = questionServices;
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
                        QuestionImage = question.QuestionImage,
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