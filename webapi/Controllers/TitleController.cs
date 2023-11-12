using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/title")]
    public class TitleController : ControllerBase
    {
        private readonly TitleServices _titleServices;

        public TitleController(TitleServices titleServices)
        {
            _titleServices = titleServices;
        }

        [HttpGet]
        [Route("getAllTitles")]
        public async Task<IActionResult> GetAllTitles()
        {
            try
            {
                List<Title> titles = await _titleServices.GetAllTitles();
                return Ok(titles);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getTitleById/{id}")]
        public async Task<IActionResult> GetTitleById(String id)
        {
            try
            {
                List<Title> titles = await _titleServices.GetTitleById(id);

                if (titles.Count ==  0)
                {
                    return NotFound(new
                    {
                        error = "No user found !"
                    });
                }
                else
                    return Ok(titles[0]);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updateTitle/{id}")]
        public async Task<IActionResult> UpdateTitle(String id, [FromBody] Title title)
        {
            try
            {
                await _titleServices.UpdateTitle(id, title);

                return Ok(new
                {
                    success = "Update title successfully !"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteTitle/{id}")]
        public async Task<IActionResult> DeleteTitle(String id)
        {
            try
            {
                await _titleServices.DeleteTitle(id);

                return Ok(new
                {
                    success = "Delete title successfully !"
                });
            }
            catch
            {
                throw;
            }
        }
    }
}