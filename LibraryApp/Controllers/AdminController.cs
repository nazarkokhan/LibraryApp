using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
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
        public async Task<ActionResult<IEnumerable<User>>> GetUsersPage(string? search, int page = 1, int items = 5)
        {
            return Ok(await _adminService.GetUsersPageAsync(search, page, items));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser([Range(0, int.MaxValue)] int id)
        {
            return Ok(await _adminService.GetUserAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult<User>> EditUsers(EditUserDto userDto)
        {
            return Ok(await _adminService.EditUserAsync(userDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsers([Range(0, int.MaxValue)] int id)
        {
            await _adminService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}