using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.DAL.Entities;
using LibraryApp.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role = LibraryApp.Core.ResultConstants.AuthorizationConstants.Role;

namespace LibraryApp.Controllers
{
    [BearerAuthorize(Role.Admin)]
    [ApiController]
    [Route("api/[controller]/users")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPage(
            [FromQuery] string? search,
            [FromQuery][Range(1, int.MaxValue)] int page = 1, 
            [FromQuery] int items = 5)
        {
            return (await _adminService.GetUsersPageAsync(search, page, items)).ToActionResult();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser([Range(0, int.MaxValue)] int id)
        {
            return (await _adminService.GetUserAsync(id)).ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> EditUsers(EditUserDto userDto)
        {
            return (await _adminService.EditUserAsync(userDto)).ToActionResult();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsers([Range(0, int.MaxValue)] int id) 
        {
            return (await _adminService.DeleteUserAsync(id)).ToActionResult();
        }
    }
}