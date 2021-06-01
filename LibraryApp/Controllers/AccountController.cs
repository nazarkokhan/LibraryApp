using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/account")]
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

            var result = await _userManager.CreateAsync(user, register.Password);


            return BadRequest();
        }


        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            var email = User.FindFirst(ClaimTypes.Email);

            return Ok();
        }


        [HttpPost("token")]
        public async Task<IActionResult> Token(LogInUserDto userInput)
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
                    new (ClaimTypes.NameIdentifier, user.Id.ToString())
                },
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(LogInUserDto userInput)
        {
            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Bearer");

                return claimsIdentity;
            }

            return null;
        }
    }
}