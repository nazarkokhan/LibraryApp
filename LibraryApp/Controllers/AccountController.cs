using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
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

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync(LogInUserDto userDto)
        {
            return (await _accountService.GetAccessTokenAsync(userDto)).ToActionResult();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            return (await _accountService.RegisterAsync(register)).ToActionResult();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return (_accountService.GetProfile()).ToActionResult();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("reset/email/token")]
        public async Task<IActionResult> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto)
        {
            return (await _accountService.SendEmailResetTokenAsync(resetEmailDto)).ToActionResult();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("reset/email")]
        public async Task<IActionResult> ResetEmailAsync(TokenEmailDto tokenEmailDto)
        {
            return (await _accountService.ResetEmailAsync(tokenEmailDto)).ToActionResult();
        }

        [HttpGet("reset/password/token")]
        public async Task<IActionResult> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto)
        {
            return (await _accountService.SendPasswordResetTokenAsync(resetPasswordDto)).ToActionResult();
        }

        [HttpPut("reset/password")]
        public async Task<IActionResult> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto)
        {
            return (await _accountService.ResetPasswordAsync(tokenPasswordDto)).ToActionResult();
        }
    }
}