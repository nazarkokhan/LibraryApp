namespace LibraryApp.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Abstract;
using BLL.Services.Abstraction;
using Core.DTO.Authorization;
using Core.ResultConstants.AuthorizationConstants;
using Extensions;
using Microsoft.AspNetCore.Mvc;

[BearerAuthorize(AccessRole.Admin)]
public class AdminController : ApiController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersPage(
        [FromQuery] string? search,
        [FromQuery] [Range(1, int.MaxValue)] int page = 1,
        [FromQuery] int items = 5
    )
        => (await _adminService.GetUsersPageAsync(search, page, items)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser([Range(0, int.MaxValue)] int id) 
        => (await _adminService.GetUserAsync(id)).ToActionResult();

    [HttpPut]
    public async Task<IActionResult> EditUsers(EditUserDto userDto) 
        => (await _adminService.EditUserAsync(userDto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUsers([Range(0, int.MaxValue)] int id) 
        => (await _adminService.DeleteUserAsync(id)).ToActionResult();
}