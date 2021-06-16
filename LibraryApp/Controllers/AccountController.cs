using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register/token")]
        public async Task<IActionResult> SendRegisterToken(RegisterDto register)
        {
            return (await _accountService.SendRegisterTokenAsync(register)).ToActionResult();
        }
        
        [HttpGet("register")]
        public async Task<IActionResult> ConfirmRegistration(string token, string userId)
        {
            return (await _accountService.ConfirmRegistrationAsync(token, userId)).ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync(LogInUserDto userDto)
        {
            return (await _accountService.GetAccessTokenAsync(userDto, User.GetUserId())).ToActionResult();
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            return (await _accountService.GetProfile(User.GetUserId())).ToActionResult();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("reset-email/email-token")]
        public async Task<IActionResult> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto)
        {
            return (await _accountService.SendEmailResetTokenAsync(resetEmailDto)).ToActionResult();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("reset-email")]
        public async Task<IActionResult> ResetEmailAsync(TokenEmailDto tokenEmailDto)
        {
            return (await _accountService.ResetEmailAsync(tokenEmailDto)).ToActionResult();
        }

        [HttpGet("reset-password/email-token")]
        public async Task<IActionResult> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto)
        {
            return (await _accountService.SendPasswordResetTokenAsync(resetPasswordDto)).ToActionResult();
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto)
        {
            return (await _accountService.ResetPasswordAsync(tokenPasswordDto)).ToActionResult();
        }
    }
}