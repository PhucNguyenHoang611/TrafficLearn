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
        private readonly FileServices _fileServices;
        private readonly UserServices _userServices;

        public NewsController(NewsServices NewsServices, FileServices fileServices, UserServices userServices)
        {
            _newsServices = NewsServices;
            _fileServices = fileServices;
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
                        Id = ObjectId.GenerateNewId().ToString(),
                        NewsTitle = news.NewsTitle,
                        NewsClarify = news.NewsClarify,
                        NewsDate = news.NewsDate,
                        NewsContent = news.NewsContent
                    };

                    await _newsServices.CreateNews(n);

                    return Ok(new
                    {
                        success = "Create news successfully !",
                        newsId = n.Id
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
        [Route("uploadNewsThumbnail/{id}")]
        [Authorize]
        public async Task<IActionResult> UploadNewsThumbnail(string id, IFormFile file)
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
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            string imageUrl = await _fileServices.UploadToFirebaseStorage(memoryStream, "NewsThumbnail_" + file.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff"));

                            news[0].NewsThumbnail = imageUrl;
                            await _newsServices.UpdateNews(id, news[0]);

                            return Ok(new
                            {
                                success = "Upload news thumbnail successfully !"
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

        [HttpPost]
        [Route("uploadNewsImage")]
        [Authorize]
        public async Task<IActionResult> UploadNewsImage(IFormFile file)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        string imageUrl = await _fileServices.UploadToFirebaseStorage(memoryStream, "NewsImage_" + file.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff"));

                        return Ok(new
                        {
                            success = "Upload news image successfully !",
                            imageURL = imageUrl
                        });
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

        [HttpPut]
        [Route("hideOrUnhideNews/{id}")]
        [Authorize]
        public async Task<IActionResult> HideOrUnhideNews(string id)
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
                    else
                    {
                        news[0].IsHidden = news[0].IsHidden ? false : true;
                        string message = news[0].IsHidden ? "Hide" : "Unhide";

                        await _newsServices.UpdateNews(id, news[0]);

                        return Ok(new
                        {
                            success = message + " news successfully !"
                        });
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