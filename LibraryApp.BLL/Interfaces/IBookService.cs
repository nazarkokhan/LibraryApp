using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Interfaces
{
    public interface IBookService
    {
        Task<Pager<GetBookDto>> GetBooksAsync(int page, int itemsOnPage);

        Task<GetBookDto> GetBookAsync(int id);

        Task<GetBookDto> CreateBookAsync(CreateBookDto book);

        Task<GetBookDto> UpdateBookAsync(UpdateBookDto book);

        Task DeleteBookAsync(int id);
    }
}
