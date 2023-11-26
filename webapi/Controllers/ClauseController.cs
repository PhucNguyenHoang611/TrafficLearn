using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/clause")]
    public class ClauseController : ControllerBase
    {
        private readonly ClauseServices _clauseServices;
        private readonly UserServices _userServices;

        public ClauseController(ClauseServices clauseServices, UserServices userServices)
        {
            _clauseServices = clauseServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllClauses")]
        public async Task<IActionResult> GetAllClauses()
        {
            try
            {
                List<Clause> clauses = await _clauseServices.GetAllClauses();
                return Ok(clauses);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getClauseById/{id}")]
        public async Task<IActionResult> GetClauseById(string id)
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

                List<Clause> clauses = await _clauseServices.GetClauseById(id);

                if (clauses.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No clause found !"
                    });
                }
                else
                    return Ok(clauses[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createClause")]
        [Authorize]
        public async Task<IActionResult> CreateClause([FromBody] Clause clause)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(clause.ArticleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid article ID !"
                        });
                    }

                    Clause c = new Clause
                    {
                        ArticleId = clause.ArticleId,
                        ClauseTitle = clause.ClauseTitle
                    };

                    await _clauseServices.CreateClause(c);

                    return Ok(new
                    {
                        success = "Create clause successfully !"
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
        [Route("updateClause/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateClause(string id, [FromBody] Clause clause)
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

                    List<Clause> clauses = await _clauseServices.GetClauseById(id);

                    if (clauses.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No clause found !"
                        });
                    }

                    if (!ObjectId.TryParse(clause.ArticleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid article ID !"
                        });
                    }

                    await _clauseServices.UpdateClause(id, clause);

                    return Ok(new
                    {
                        success = "Update clause successfully !"
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
        [Route("deleteClause/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteClause(string id)
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

                    List<Clause> clauses = await _clauseServices.GetClauseById(id);

                    if (clauses.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No clause found !"
                        });
                    }

                    await _clauseServices.DeleteClause(id);

                    return Ok(new
                    {
                        success = "Delete clause successfully !"
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