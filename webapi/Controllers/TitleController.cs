﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/title")]
    public class TitleController : ControllerBase
    {
        private readonly TitleServices _titleServices;
        private readonly UserServices _userServices;

        public TitleController(TitleServices titleServices, UserServices userServices)
        {
            _titleServices = titleServices;
            _userServices = userServices;
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

        [HttpPost]
        [Route("createTitle")]
        [Authorize]
        public async Task<IActionResult> CreateTitle([FromBody] Title title)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    Title t = new Title
                    {
                        TitleName = title.TitleName
                    };

                    await _titleServices.CreateTitle(t);

                    return Ok(new
                    {
                        success = "Create title successfully !"
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
        [Route("updateTitle/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTitle(String id, [FromBody] Title title)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                string userRole = await _userServices.JwtAuthentication(identity);

                if (userRole == "Admin")
                {
                    await _titleServices.UpdateTitle(id, title);

                    return Ok(new
                    {
                        success = "Update title successfully !"
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
        [Route("deleteTitle/{id}")]
        [Authorize]
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