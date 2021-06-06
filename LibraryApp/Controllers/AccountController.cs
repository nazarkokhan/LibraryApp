using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager, IAccountService accountService)
        {
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            await _accountService.RegisterAsync(register);

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public ActionResult<UserFromTokenDto> GetProfile()
        {
            var user = new UserFromTokenDto
            {
                Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                Email = User.FindFirst(ClaimTypes.Email)!.Value
            };

            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("reset/email")]
        public async Task<ActionResult> ResetEmailAsync(ChangeEmailDto emailDto)
        {
            await _accountService.ChangeEmailAsync(emailDto);

            return Ok();
        }

        [HttpPut("reset/password")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto userDto)
        {
            await _accountService.ResetPasswordAsync(userDto);

            return Ok();
        }


        [HttpPost("LogIn")]
        public async Task<ActionResult<string>> LogInAsync(LogInUserDto userDto)
        {
            return Ok(await _accountService.LogInAsync(userDto));
        }
    }
}