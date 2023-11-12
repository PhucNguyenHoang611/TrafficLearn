using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/license")]
    public class LicenseController : ControllerBase
    {
        private readonly LicenseServices _licenseServices;

        public LicenseController(LicenseServices licenseServices)
        {
            _licenseServices = licenseServices;
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
        public async Task<IActionResult> GetLicenseById(String id)
        {
            try
            {
                List <License> licenses = await _licenseServices.GetLicenseById(id);

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

        [HttpPut]
        [Route("updateLicense/{id}")]
        public async Task<IActionResult> UpdateLicense(String id, [FromBody] License license)
        {
            try
            {
                await _licenseServices.UpdateLicense(id, license);

                return Ok(new
                {
                    success = "Update license successfully !"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteLicense/{id}")]
        public async Task<IActionResult> DeleteLicense(String id)
        {
            try
            {
                await _licenseServices.DeleteLicense(id);

                return Ok(new
                {
                    success = "Delete license successfully !"
                });
            }
            catch
            {
                throw;
            }
        }
    }
}