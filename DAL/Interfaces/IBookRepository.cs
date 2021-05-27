using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.DAL.DTO;

namespace LibraryApp.DAL.Interfaces
{
    public interface IBookRepository
    {
        Task<Pager<GetBookDto>> GetBooksAsync(int page);

        Task<GetBookDto> GetBookAsync(int id);

        Task<GetBookDto> CreateBookAsync(CreateBookDto book);

        Task<GetBookDto> UpdateBookAsync(UpdateBookDto book);

        Task DeleteBookAsync(int id);
    }
}