using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/license")]
    public class LicenseController : ControllerBase
    {
        private readonly LicenseServices _licenseServices;
        private readonly UserServices _userServices;

        public LicenseController(LicenseServices licenseServices, UserServices userServices)
        {
            _licenseServices = licenseServices;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("getAllLicenses")]
        public async Task<IActionResult> GetAllLicenses()
        {
            try
            {
                List<License> licenses = await _licenseServices.GetAllLicenses();
                return Ok(licenses);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getLicenseById/{id}")]
        public async Task<IActionResult> GetLicenseById(string id)
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

                List<License> licenses = await _licenseServices.GetLicenseById(id);

                if (licenses.Count == 0)
                {
                    return NotFound(new
                    {
                        error = "No license found !"
                    });
                }
                else
                    return Ok(licenses[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createLicense")]
        [Authorize]
        public async Task<IActionResult> CreateLicense([FromBody] License license)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    License l = new License
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        LicenseName = license.LicenseName
                    };

                    await _licenseServices.CreateLicense(l);

                    return Ok(new
                    {
                        success = "Create license successfully !",
                        licenseId = l.Id
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
        [Route("updateLicense/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLicense(string id, [FromBody] License license)
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

                    List<License> licenses = await _licenseServices.GetLicenseById(id);

                    if (licenses.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No license found !"
                        });
                    }

                    await _licenseServices.UpdateLicense(id, license);

                    return Ok(new
                    {
                        success = "Update license successfully !"
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
        [Route("deleteLicense/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLicense(string id)
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

                    List<License> licenses = await _licenseServices.GetLicenseById(id);

                    if (licenses.Count == 0)
                    {
                        return NotFound(new
                        {
                            error = "No license found !"
                        });
                    }

                    await _licenseServices.DeleteLicense(id);

                    return Ok(new
                    {
                        success = "Delete license successfully !"
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