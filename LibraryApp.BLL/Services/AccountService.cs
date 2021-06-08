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
using LibraryApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        private readonly IEmailService _emailService;
        
        private readonly IHttpContextAccessor _http;

        public AccountService(UserManager<User> userManager, IHttpContextAccessor http, IEmailService emailService)
        {
            _userManager = userManager;
            _http = http;
            _emailService = emailService;
        }

        public async Task RegisterAsync(RegisterDto register)
        {
            var user = new User {Email = register.Email, UserName = register.Email, Age = register.Age};

            await _userManager.CreateAsync(user, register.Password);

            await _userManager.AddToRoleAsync(user, Roles.User);

            await _emailService.SendAsync(user.Email,
                "You have successfully created your account in library!",
                $"Thanks for your registration, {user.UserName}.");
        }

        public async Task<string> GetAccessTokenAsync(LogInUserDto userInput)
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
                    new (ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault()!)
                },
                expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public UserFromTokenDto GetProfile()
        {
            if (_http.HttpContext is null)
            {
                throw new NotSupportedException();
            }

            int.TryParse(_http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);
            
            var user = new UserFromTokenDto
            {
                Id = id,
                Email = _http.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value,
                Role = _http.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value,
            };

            return user;
        }

        public async Task SendEmailResetTokenAsync(ResetEmailDto resetEmailDto)
        {
            var userEntity = await _userManager
                .FindByEmailAsync(_http.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty);
            
            var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(userEntity, resetEmailDto.NewEmail);

            await _emailService.SendAsync(userEntity.Email, changeEmailToken, "You want to change Email");
        }

        public async Task ResetEmailAsync(TokenEmailDto tokenEmailDto)
        {
            var userEntity = await _userManager
                .FindByEmailAsync(_http.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty);

            await _userManager.ChangeEmailAsync(userEntity, tokenEmailDto.NewEmail, tokenEmailDto.Token);
        }

        public async Task SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userEntity);

            await _emailService.SendAsync(resetPasswordDto.Email, passwordResetToken, "You want to change Password");
        }

        public async Task ResetPasswordAsync(TokenPasswordDto tokenPasswordDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(tokenPasswordDto.Email);

            await _userManager.ResetPasswordAsync(userEntity, tokenPasswordDto.Token, tokenPasswordDto.NewPassword);
        }
    }
}