using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<Pager<GetAuthorDto>>> GetAuthorsAsync([FromQuery] string search, [FromQuery]int page = 1, [FromQuery]int items = 5)
        {
            return Ok(await _authorService.GetAuthorsAsync(page, items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAuthorDto>> GetAuthorAsync(int id)
        {
            return Ok(await _authorService.GetAuthorAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetAuthorDto>> CreateAuthorAsync(CreateAuthorDto author)
        {
            return Ok(await _authorService.CreateAuthorAsync(author));
        }

        [HttpPut]
        public async Task<ActionResult<GetAuthorDto>> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            return Ok(await _authorService.UpdateAuthorAsync(author));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorAsync(int id)
        {
            await _authorService.DeleteAuthorAsync(id);

            return NoContent();
        }
    }
}