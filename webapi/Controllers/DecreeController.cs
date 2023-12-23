using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class ClauseDetails
    {
        public Clause Clause { get; set; } = null!;
        public List<Point> PointsList { get; set; } = new List<Point>();
    }
    public class ArticleDetails
    {
        public Article Article { get; set; } = null!;
        public List<ClauseDetails> ClausesList { get; set; } = new List<ClauseDetails>();
    }

    [ApiController]
    [Route("api/decree")]
    public class DecreeController : ControllerBase
    {
        private readonly DecreeServices _decreeServices;
        private readonly ArticleServices _articleServices;
        private readonly ClauseServices _clauseServices;
        private readonly PointServices _pointServices;
        private readonly UserServices _userServices;

        public DecreeController(DecreeServices decreeServices, ArticleServices articleServices, ClauseServices clauseServices, PointServices pointServices, UserServices userServices)
        {
            _decreeServices = decreeServices;
            _articleServices = articleServices;
            _clauseServices = clauseServices;
            _pointServices = pointServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllDecrees")]
        public async Task<IActionResult> GetAllDecrees()
        {
            try
            {
                List<Decree> decrees = await _decreeServices.GetAllDecrees();
                return Ok(decrees);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getDecreeById/{id}")]
        public async Task<IActionResult> GetDecreeById(string id)
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

                List<Decree> decrees = await _decreeServices.GetDecreeById(id);

                if (decrees.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No decree found !"
                    });
                }
                else
                    return Ok(decrees[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllDecreeDetails/{id}")]
        public async Task<IActionResult> GetAllDecreeDetails(string id)
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

                List<Decree> decrees = await _decreeServices.GetDecreeById(id);
                List<ArticleDetails> articles = new List<ArticleDetails>();

                if (decrees.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No decree found !"
                    });
                }
                else
                {
                    List<Article> aList = await _articleServices.GetArticlesByDecreeId(id);

                    for (int i = 0; i < aList.Count; i++)
                    {
                        ArticleDetails ad = new ArticleDetails();

                        ad.Article = aList[i];

                        List<Clause> cList = await _clauseServices.GetClausesByArticleId(aList[i].Id);

                        for (int j = 0; j < cList.Count; j++)
                        {
                            ClauseDetails cd = new ClauseDetails();

                            cd.Clause = cList[j];
                            cd.PointsList = await _pointServices.GetPointsByClauseId(cList[j].Id);

                            ad.ClausesList.Add(cd);
                        }

                        articles.Add(ad);
                    }

                    return Ok(new
                    {
                        success = "Get decree details successfully !",
                        decree = decrees[0],
                        details = articles
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createDecree")]
        [Authorize]
        public async Task<IActionResult> CreateDecree([FromBody] Decree decree)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    Decree d = new Decree
                    {
                        DecreeName = decree.DecreeName,
                        DecreeDate = decree.DecreeDate,
                        DecreeNumber = decree.DecreeNumber
                    };

                    await _decreeServices.CreateDecree(d);

                    return Ok(new
                    {
                        success = "Create decree successfully !"
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
        [Route("updateDecree/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDecree(string id, [FromBody] Decree decree)
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

                    List<Decree> decrees = await _decreeServices.GetDecreeById(id);

                    if (decrees.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No decree found !"
                        });
                    }

                    await _decreeServices.UpdateDecree(id, decree);

                    return Ok(new
                    {
                        success = "Update decree successfully !"
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
        [Route("deleteDecree/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDecree(string id)
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

                    List<Decree> decrees = await _decreeServices.GetDecreeById(id);

                    if (decrees.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No decree found !"
                        });
                    }

                    await _decreeServices.DeleteDecree(id);

                    return Ok(new
                    {
                        success = "Delete decree successfully !"
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