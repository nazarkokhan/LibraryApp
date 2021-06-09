using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IBookService
    {
        Task<Pager<BookDto>> GetBooksAsync(int page, int itemsOnPage, string? search);

        Task<BookDto> GetBookAsync(int id);

        Task<BookDto> CreateBookAsync(CreateBookDto book);

        Task<BookDto> UpdateBookAsync(UpdateBookDto book);

        Task DeleteBookAsync(int id);
    }
}