using System.Threading.Tasks;
using LibraryApp.DAL.DTO;

namespace LibraryApp.BLL.Interfaces
{
    public interface IBookService
    {
        Task<Pager<GetBookDto>> GetBooksAsync(int page);

        Task<GetBookDto> GetBookAsync(int id);

        Task<GetBookDto> CreateBookAsync(CreateBookDto book);

        Task<GetBookDto> UpdateBookAsync(UpdateBookDto book);

        Task DeleteBookAsync(int id);
    }
}
