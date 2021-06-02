using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("users/{id:int}")]
        public async Task<ActionResult<User>> EditUsers([Range(0, int.MaxValue)]int id)
        {
            return Ok(await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id));
        }

        [HttpPut("users")]
        public async Task<ActionResult> EditUsers(EditUserDto userDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(userDto.CurrentEmail);

            var token = await _userManager.GenerateChangeEmailTokenAsync(userEntity, userDto.NewEmail);

            //userEntity.Age = userDto.Age;

            var changeEmail = await _userManager.ChangeEmailAsync(userEntity, userEntity.Email, token);

            var changePassword = await _userManager.ChangePasswordAsync(userEntity, userDto.CurrentPassword, userDto.NewPassword);

            if (changeEmail.Succeeded && changePassword.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("users/{id:int}")]
        public async Task<ActionResult> DeleteUsers([Range(0, int.MaxValue)] int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            await _userManager.DeleteAsync(user);

            return NoContent();
        }
    }
}