using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly NewsServices _newsServices;
        private readonly UserServices _userServices;

        public NewsController(NewsServices NewsServices, UserServices userServices)
        {
            _newsServices = NewsServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllNews")]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                List<News> news = await _newsServices.GetAllNews();
                return Ok(news);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getNewsById/{id}")]
        public async Task<IActionResult> GetNewsById(string id)
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

                List<News> news = await _newsServices.GetNewsById(id);

                if (news.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No news found !"
                    });
                }
                else
                    return Ok(news[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createNews")]
        [Authorize]
        public async Task<IActionResult> CreateNews([FromBody] News news)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    News n = new News
                    {
                        NewsTitle = news.NewsTitle,
                        NewsClarify = news.NewsClarify,
                        NewsDate = news.NewsDate,
                        NewsContent = news.NewsContent,
                        NewsImage = news.NewsImage,
                        NewsImageTitle = news.NewsImageTitle
                    };

                    await _newsServices.CreateNews(n);

                    return Ok(new
                    {
                        success = "Create news successfully !"
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
        [Route("updateNews/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateNews(string id, [FromBody] News news)
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

                    List<News> newsList = await _newsServices.GetNewsById(id);

                    if (newsList.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No news found !"
                        });
                    }

                    await _newsServices.UpdateNews(id, news);

                    return Ok(new
                    {
                        success = "Update news successfully !"
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
        [Route("deleteNews/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNews(string id)
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

                    List<News> news = await _newsServices.GetNewsById(id);

                    if (news.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No news found !"
                        });
                    }

                    await _newsServices.DeleteNews(id);

                    return Ok(new
                    {
                        success = "Delete news successfully !"
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