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
        private readonly ClauseServices _clauseServices;
        private readonly PointServices _pointServices;
        private readonly UserServices _userServices;

        public ArticleController(ArticleServices ArticleServices, ClauseServices clauseServices, PointServices pointServices, UserServices userServices)
        {
            _articleServices = ArticleServices;
            _clauseServices = clauseServices;
            _pointServices = pointServices;
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
        [Route("getArticlesByDecreeId/{id}")]
        public async Task<IActionResult> GetArticlesByDecreeId(string id)
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

                List<Article> articles = await _articleServices.GetArticlesByDecreeId(id);
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

        [HttpGet]
        [Route("getAllArticleDetails/{id}")]
        public async Task<IActionResult> GetAllArticleDetails(string id)
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
                List<ClauseDetails> clauses = new List<ClauseDetails>();

                if (articles.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No article found !"
                    });
                }
                else
                {
                    List<Clause> cList = await _clauseServices.GetClausesByArticleId(id);

                    for (int i = 0;  i < cList.Count; i++)
                    {
                        ClauseDetails cd = new ClauseDetails();

                        cd.Clause = cList[i];
                        cd.PointsList = await _pointServices.GetPointsByClauseId(cList[i].Id);

                        clauses.Add(cd);
                    }

                    return Ok(new
                    {
                        success = "Get article details successfully !",
                        article = articles[0],
                        details = clauses
                    });
                }
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