using System.Threading.Tasks;
using LibraryApp.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

using Abstract;
using BLL.Services.Abstraction;
using Core.DTO.Authorization;
using Core.DTO.Authorization.Reset;
using Core.ResultConstants.AuthorizationConstants;

public class AccountController : ApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register/token")]
    public async Task<IActionResult> CreteUserAndSendEmailToken(RegisterDto register) 
        => (await _accountService.CreateUserAndSendEmailTokenAsync(register)).ToActionResult();

    [HttpGet("register")]
    public async Task<IActionResult> ConfirmRegistration(string token, string userId) 
        => (await _accountService.ConfirmRegistrationAsync(token, userId)).ToActionResult();

    [HttpPost("login")]
    public async Task<IActionResult> LogInAsync(LogInUserDto userDto) 
        => (await _accountService.GetAccessTokenAsync(userDto)).ToActionResult();

    [BearerAuthorize(AccessRole.Admin | AccessRole.User)]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile() 
        => (await _accountService.GetProfile(User.GetUserId())).ToActionResult();

    [BearerAuthorize(AccessRole.Admin | AccessRole.User)]
    [HttpPut("reset-email/email-token")]
    public async Task<IActionResult> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto) 
        => (await _accountService.SendEmailResetTokenAsync(resetEmailDto, User.GetUserId())).ToActionResult();

    [BearerAuthorize(AccessRole.Admin | AccessRole.User)]
    [HttpGet("reset-email")]
    public async Task<IActionResult> ResetEmailAsync(string token, string newEmail) 
        => (await _accountService.ResetEmailAsync(token, newEmail, User.GetUserId())).ToActionResult();

    [HttpPut("reset-password/email-token")]
    public async Task<IActionResult> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto) 
        => (await _accountService.SendPasswordResetTokenAsync(resetPasswordDto)).ToActionResult();

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto) 
        => (await _accountService.ResetPasswordAsync(tokenPasswordDto)).ToActionResult();
}