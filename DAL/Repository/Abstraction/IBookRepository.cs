using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Book;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.DAL.Repository.Abstraction
{
    public interface IBookRepository
    {
        Task<Result<Pager<BookDto>>> GetBooksAsync(int page, int itemsOnPage, string? search);

        Task<Result<BookDto>> GetBookAsync(int id);

        Task<Result<BookDto>> CreateBookAsync(CreateBookDto book);

        Task<Result<BookDto>> UpdateBookAsync(UpdateBookDto book);

        Task<Result> DeleteBookAsync(int id);
    }
}