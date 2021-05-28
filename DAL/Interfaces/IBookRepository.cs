using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.DAL.Interfaces
{
    public interface IBookRepository
    {
        Task<Pager<GetBookDto>> GetBooksAsync(int page, int itemsOnPage);

        Task<GetBookDto> GetBookAsync(int id);

        Task<GetBookDto> CreateBookAsync(CreateBookDto book);

        Task<GetBookDto> UpdateBookAsync(UpdateBookDto book);

        Task DeleteBookAsync(int id);
    }
}