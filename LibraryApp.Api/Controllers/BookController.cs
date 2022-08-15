using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

using Abstract;
using BLL.Services.Abstraction;
using Core.DTO.Book;
using Core.ResultConstants.AuthorizationConstants;

[BearerAuthorize(AccessRole.User | AccessRole.Admin)]
public class BookController : ApiController
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksAsync(
        [FromQuery] string? search,
        [FromQuery] [Range(1, int.MaxValue)] int page = 1,
        [FromQuery] int items = 5
    )
        => (await _bookService.GetBooksAsync(page, items, search)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookAsync([Range(0, int.MaxValue)] int id)
        => (await _bookService.GetBookAsync(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(CreateBookDto book)
        => (await _bookService.CreateBookAsync(book)).ToActionResult();

    [HttpPut]
    public async Task<IActionResult> UpdateBookAsync(UpdateBookDto book)
        => (await _bookService.UpdateBookAsync(book)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBookAsync([Range(0, int.MaxValue)] int id)
        => (await _bookService.DeleteBookAsync(id)).ToActionResult();
}