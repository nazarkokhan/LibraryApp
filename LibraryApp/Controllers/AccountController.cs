using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
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
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            var user = new User { Email = register.Email, UserName = register.Email, Age = register.Age };

            var identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("profile")]
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
        public async Task<ActionResult> ResetEmailAsync(ResetEmailDto emailDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(emailDto.OldEmail);

            var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(userEntity, emailDto.NewEmail);

            var changeEmail = await _userManager.ChangeEmailAsync(userEntity, emailDto.NewEmail, changeEmailToken);

            if (changeEmail.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("reset/password")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto userDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(userDto.Email);

            var changePasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userEntity);

            await _userManager.ResetPasswordAsync(userEntity, changePasswordToken, userDto.NewPassword);

            var changePassword = await _userManager.ResetPasswordAsync(userEntity, changePasswordToken, userDto.NewPassword);

            if (changePassword.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("token")]
        public async Task<ActionResult> Token(LogInUserDto userInput)
        {
            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, userInput.Password))
                return BadRequest();

            var timeNow = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: timeNow,
                claims: new List<Claim>
                {
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.FirstOrDefault()!)
                },
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));



            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }
    }
}