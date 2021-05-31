using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{page:int}/{items:int}")]
        public async Task<ActionResult<Pager<GetBookDto>>> GetBooksAsync([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int items = 5)
        {
            return await _bookService.GetBooksAsync(page, items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookDto>> GetBookAsync(int id)
        {
            return Ok(await _bookService.GetBookAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GetBookDto>> CreateBookAsync(CreateBookDto book)
        {
            return Ok(await _bookService.CreateBookAsync(book));
        }

        [HttpPut]
        public async Task<ActionResult<GetBookDto>> UpdateBookAsync(UpdateBookDto book)
        {
            return Ok(await _bookService.UpdateBookAsync(book));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookAsync(int id)
        {
            await _bookService.DeleteBookAsync(id);

            return Ok();
        }
    }
}