using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _user;
        public AccountController(IUserService user)
        {
            _user = user;
        }

        [HttpPost("/token")]
        public ActionResult Token(LogInUserDto userInput)
        {
            var identity = GetIdentity(userInput);

            if (identity == null)
            {
                return BadRequest(new {errorText = "Invalid username or password."});
            }

            var timeNow = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: timeNow,
                claims: identity.Claims,
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(LogInUserDto userInput)
        {
            GetUserDto user = _user
                .GetUsersPageAsync(1, int.MaxValue, null).Result.Data
                .FirstOrDefault(u => u.Login == userInput.Login && u.Password == userInput.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new (ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new (ClaimsIdentity.DefaultRoleClaimType, user.Admin.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
}