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
        public Task<Pager<GetAuthorDto>> GetAuthorsAsync([FromQuery]int page = 1, [FromQuery]int items = 5)
        {
            return _authorService.GetAuthorsAsync(page, items);
        }

        [HttpGet("{id}")]
        public Task<GetAuthorDto> GetAuthorAsync(int id)
        {
            return _authorService.GetAuthorAsync(id);
        }

        [HttpPost]
        public Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author)
        {
            return _authorService.CreateAuthorAsync(author);
        }

        [HttpPut]
        public Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            return _authorService.UpdateAuthorAsync(author);
        }

        [HttpDelete("{id}")]
        public Task DeleteAuthorAsync(int id)
        {
            return _authorService.DeleteAuthorAsync(id);
        }
    }
}