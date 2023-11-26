using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/licenseTitle")]
    public class LicenseTitleController : ControllerBase
    {
        private readonly LicenseTitleServices _licenseTitleServices;
        private readonly UserServices _userServices;

        public LicenseTitleController(LicenseTitleServices licenseTitleServices, UserServices userServices)
        {
            _licenseTitleServices = licenseTitleServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllLicenseTitles")]
        public async Task<IActionResult> GetAllLicenseTitles()
        {
            try
            {
                List<LicenseTitle> licenseTitles = await _licenseTitleServices.GetAllLicenseTitles();
                return Ok(licenseTitles);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getLicenseTitleById/{id}")]
        public async Task<IActionResult> GetLicenseTitleById(string id)
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

                List<LicenseTitle> licenseTitles = await _licenseTitleServices.GetLicenseTitleById(id);

                if (licenseTitles.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No license title found !"
                    });
                }
                else
                    return Ok(licenseTitles[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createLicenseTitle")]
        [Authorize]
        public async Task<IActionResult> CreateLicenseTitle([FromBody] LicenseTitle licenseTitle)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    if (!ObjectId.TryParse(licenseTitle.LicenseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license ID !"
                        });
                    }

                    if (!ObjectId.TryParse(licenseTitle.TitleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid title ID !"
                        });
                    }

                    LicenseTitle lt = new LicenseTitle
                    {
                        LicenseId = licenseTitle.LicenseId,
                        TitleId = licenseTitle.TitleId
                    };

                    await _licenseTitleServices.CreateLicenseTitle(lt);

                    return Ok(new
                    {
                        success = "Create license title successfully !"
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
        [Route("updateLicenseTitle/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLicenseTitle(string id, [FromBody] LicenseTitle licenseTitle)
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

                    List<LicenseTitle> licenseTitles = await _licenseTitleServices.GetLicenseTitleById(id);

                    if (licenseTitles.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No license title found !"
                        });
                    }

                    if (!ObjectId.TryParse(licenseTitle.LicenseId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid license ID !"
                        });
                    }

                    if (!ObjectId.TryParse(licenseTitle.TitleId, out _))
                    {
                        return BadRequest(new
                        {
                            error = "Invalid title ID !"
                        });
                    }

                    await _licenseTitleServices.UpdateLicenseTitle(id, licenseTitle);

                    return Ok(new
                    {
                        success = "Update license title successfully !"
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
        [Route("deleteLicenseTitle/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLicenseTitle(string id)
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

                    List<LicenseTitle> licenseTitles = await _licenseTitleServices.GetLicenseTitleById(id);

                    if (licenseTitles.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No license title found !"
                        });
                    }

                    await _licenseTitleServices.DeleteLicenseTitle(id);

                    return Ok(new
                    {
                        success = "Delete license title successfully !"
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