using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterAsync(RegisterDto register)
        {
            var user = new User { Email = register.Email, UserName = register.Email, Age = register.Age };

            await _userManager.CreateAsync(user, register.Password);
        }

        public async Task<string> LogInAsync(LogInUserDto userInput)
        {
            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, userInput.Password))
                return null;

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

            return encodedJwt;
        }

        public async Task ResetEmailAsync(ChangeEmailDto emailDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(emailDto.OldEmail);

            var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(userEntity, emailDto.NewEmail);

            userEntity.UserName = emailDto.NewEmail;

            await _userManager.ChangeEmailAsync(userEntity, emailDto.NewEmail, changeEmailToken);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto userDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(userDto.Email);

            var changePasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userEntity);

            await _userManager.ResetPasswordAsync(userEntity, changePasswordToken, userDto.NewPassword);
        }
    }
}