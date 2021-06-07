using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
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
            return Ok(await _accountService.LogInAsync(userDto));
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            await _accountService.RegisterAsync(register);

            return Ok();
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpGet("profile")]
        public ActionResult<UserFromTokenDto> GetProfile()
        {
            var user = new UserFromTokenDto
            {
                Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                Email = User.FindFirst(ClaimTypes.Email)!.Value,
                Role = User.FindFirst(ClaimTypes.Role)!.Value,
            };

            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("reset/email")]
        public async Task<ActionResult> ResetEmailAsync(ChangeEmailDto emailDto)
        {
            await _accountService.ResetEmailAsync(emailDto);

            return Ok();
        }

        [HttpPut("reset/password")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto userDto)
        {
            await _accountService.ResetPasswordAsync(userDto);

            return Ok();
        }
    }
}