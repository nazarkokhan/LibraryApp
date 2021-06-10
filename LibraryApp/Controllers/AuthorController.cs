using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<Pager<AuthorDto>>> GetAuthorsAsync([FromQuery] string? search,
            [FromQuery] int page = 1, [FromQuery] int items = 5)
        {
            return Ok(await _authorService.GetAuthorsAsync(page, items, search));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(int id)
        {
            return Ok(await _authorService.GetAuthorAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthorAsync(CreateAuthorDto author)
        {
            return Ok(await _authorService.CreateAuthorAsync(author));
        }

        [HttpPut]
        public async Task<ActionResult<AuthorDto>> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            return Ok(await _authorService.UpdateAuthorAsync(author));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAuthorAsync(int id)
        {
            await _authorService.DeleteAuthorAsync(id);

            return NoContent();
        }
    }
}