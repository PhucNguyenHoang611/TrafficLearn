using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleServices _articleServices;
        private readonly UserServices _userServices;

        public ArticleController(ArticleServices ArticleServices, UserServices userServices)
        {
            _articleServices = ArticleServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllArticles")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                List<Article> articles = await _articleServices.GetAllArticles();
                return Ok(articles);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getArticleById/{id}")]
        public async Task<IActionResult> GetArticleById(string id)
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

                List<Article> articles = await _articleServices.GetArticleById(id);

                if (articles.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No article found !"
                    });
                }
                else
                    return Ok(articles[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createArticle")]
        [Authorize]
        public async Task<IActionResult> CreateArticle([FromBody] Article article)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(article.DecreeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid decree ID !"
                        });
                    }

                    Article a = new Article
                    {
                        DecreeId = article.DecreeId,
                        ArticleTitle = article.ArticleTitle
                    };

                    await _articleServices.CreateArticle(a);

                    return Ok(new
                    {
                        success = "Create article successfully !"
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
        [Route("updateArticle/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateArticle(string id, [FromBody] Article article)
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

                    List<Article> articles = await _articleServices.GetArticleById(id);

                    if (articles.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No article found !"
                        });
                    }

                    if (!ObjectId.TryParse(article.DecreeId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid decree ID !"
                        });
                    }

                    await _articleServices.UpdateArticle(id, article);

                    return Ok(new
                    {
                        success = "Update article successfully !"
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
        [Route("deleteArticle/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(string id)
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

                    List<Article> articles = await _articleServices.GetArticleById(id);

                    if (articles.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No article found !"
                        });
                    }

                    await _articleServices.DeleteArticle(id);

                    return Ok(new
                    {
                        success = "Delete article successfully !"
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