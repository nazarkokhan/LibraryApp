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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<Pager<BookDto>>> GetBooksAsync([FromQuery] string? search,
            [FromQuery] int page = 1, [FromQuery] int items = 5)
        {
            return Ok(await _bookService.GetBooksAsync(page, items, search));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> GetBookAsync(int id)
        {
            return Ok(await _bookService.GetBookAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBookAsync(CreateBookDto book)
        {
            return Ok(await _bookService.CreateBookAsync(book));
        }

        [HttpPut]
        public async Task<ActionResult<BookDto>> UpdateBookAsync(UpdateBookDto book)
        {
            return Ok(await _bookService.UpdateBookAsync(book));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookAsync(int id)
        {
            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}