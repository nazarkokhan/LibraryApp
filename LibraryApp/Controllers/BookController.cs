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

        //[HttpGet("{page:int}/{items:int}")]
        public Task<Pager<GetBookDto>> GetBooksAsync([FromQuery] int page = 1, [FromQuery] int items = 5)
        {
            return _bookService.GetBooksAsync(page, items);
        }

        [HttpGet("{id:int}")]
        public Task<GetBookDto> GetBookAsync(int id)
        {
            return _bookService.GetBookAsync(id);
        }

        [HttpPost]
        public Task<GetBookDto> CreateBookAsync(CreateBookDto book)
        {
            return _bookService.CreateBookAsync(book);
        }

        [HttpPut]
        public Task<GetBookDto> UpdateBookAsync(UpdateBookDto book)
        {
            return _bookService.UpdateBookAsync(book);
        }

        [HttpDelete("{id:int}")]
        public Task DeleteBookAsync(int id)
        {
            return _bookService.DeleteBookAsync(id);
        }
    }
}