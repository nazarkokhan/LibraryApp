using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager, IHttpContextAccessor http, IEmailService emailService)
        {
            _userManager = userManager;
            _http = http;
            _emailService = emailService;
        }

        public async Task<Result> RegisterAsync(RegisterDto register)
        {
            try
            {
                var user = new User {Email = register.Email, UserName = register.Email, Age = register.Age};

                await _userManager.CreateAsync(user, register.Password);

                await _userManager.AddToRoleAsync(user, Roles.User);

                await _emailService.SendAsync(
                    user.Email,
                    "You have successfully created your account in library!",
                    $"Thanks for your registration, {user.UserName}."
                );

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result<Token>> GetAccessTokenAsync(LogInUserDto userInput)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userInput.Email);

                if (user is null)
                    return Result<Token>.CreateFailed(
                        AuthServiceResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                if (!await _userManager.CheckPasswordAsync(user, userInput.Password))
                    return Result<Token>.CreateFailed(AuthServiceResultConstants.InvalidUserNameOrPassword);

                var timeNow = DateTime.Now;

                var jwt = new JwtSecurityToken(
                    AuthOptions.Issuer,
                    AuthOptions.Audience,
                    notBefore: timeNow,
                    claims: new List<Claim>
                    {
                        new(ClaimTypes.Email, user.Email),
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault()!)
                    },
                    expires: timeNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey,
                        SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Result<Token>.CreateSuccess(new Token(encodedJwt));
            }
            catch (Exception e)
            {
                return Result<Token>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public Result<UserFromTokenDto> GetProfile()
        {
            try
            {
                if (_http.HttpContext is null)
                    return Result<UserFromTokenDto>.CreateFailed(
                        "HttpContext is NULL",
                        new NotSupportedException()
                    );

                var user = new UserFromTokenDto(
                    int.Parse(_http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
                    _http.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value!,
                    _http.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!
                );

                return Result<UserFromTokenDto>.CreateSuccess(user);
            }
            catch (Exception e)
            {
                return Result<UserFromTokenDto>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto)
        {
            try
            {
                var userEntity = await _userManager
                    .FindByEmailAsync(_http.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty);

                if (userEntity is null)
                    return Result.CreateFailed(
                        AuthServiceResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                var changeEmailToken = await _userManager
                    .GenerateChangeEmailTokenAsync(userEntity, resetEmailDto.NewEmail);

                // var htmlToken = $"https://localhost:5001/api/account/reset/email?newEmail={resetEmailDto.NewEmail}";

                await _emailService
                    .SendAsync(
                        userEntity.Email,
                        changeEmailToken,
                        EmailServiceAuthorizationConstants.ConfirmEmailReset
                    );

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> ResetEmailAsync(TokenEmailDto tokenEmailDto)
        {
            try
            {
                var userEntity = await _userManager
                    .FindByEmailAsync(_http.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty);

                if (userEntity is null)
                    return Result.CreateFailed(
                        AuthServiceResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                var changeEmail = await _userManager
                    .ChangeEmailAsync(userEntity, tokenEmailDto.NewEmail, tokenEmailDto.Token);

                return !changeEmail.Succeeded
                    ? Result.CreateFailed(AuthServiceResultConstants.InvalidResetEmailToken)
                    : Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var userEntity = await _userManager
                    .FindByEmailAsync(resetPasswordDto.Email);

                if (userEntity is null)
                    return Result.CreateFailed(
                        AuthServiceResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                var passwordResetToken = await _userManager
                    .GeneratePasswordResetTokenAsync(userEntity);

                await _emailService.SendAsync(
                    resetPasswordDto.Email,
                    passwordResetToken,
                    EmailServiceAuthorizationConstants.ConfirmPasswordReset
                );

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto)
        {
            try
            {
                var userEntity = await _userManager
                    .FindByEmailAsync(tokenPasswordDto.Email);

                if (userEntity is null)
                    return Result.CreateFailed(
                        AuthServiceResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                var resetPassword = await _userManager.ResetPasswordAsync(
                    userEntity,
                    $"{tokenPasswordDto.Token}",
                    tokenPasswordDto.NewPassword
                );

                return !resetPassword.Succeeded
                    ? Result.CreateFailed(AuthServiceResultConstants.InvalidResetPasswordToken)
                    : Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}