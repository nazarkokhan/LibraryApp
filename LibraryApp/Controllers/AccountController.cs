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
        public async Task<ActionResult<string>> LogInAsync(LogInUserDto userDto)
        {
            return Ok(await _accountService.GetAccessTokenAsync(userDto));
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
            var user = _accountService.GetProfile();

            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("reset/email/token")]
        public async Task<ActionResult> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto)
        {
            await _accountService.SendEmailResetTokenAsync(resetEmailDto);

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("reset/email")]
        public async Task<ActionResult> ResetEmailAsync(TokenEmailDto tokenEmailDto)
        {
            await _accountService.ResetEmailAsync(tokenEmailDto);

            return Ok();
        }

        [HttpGet("reset/password/token")]
        public async Task<ActionResult> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto)
        {
            await _accountService.SendPasswordResetTokenAsync(resetPasswordDto);

            return Ok();
        }

        [HttpPut("reset/password")]
        public async Task<ActionResult> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto)
        {
            await _accountService.ResetPasswordAsync(tokenPasswordDto);

            return Ok();
        }
    }
}