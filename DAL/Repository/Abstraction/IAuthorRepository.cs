using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.DAL.Repository.Abstraction
{
    public interface IAuthorRepository
    {
        Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int itemsOnPage);

        Task<GetAuthorDto> GetAuthorAsync(int id);

        Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author);

        Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author);

        Task DeleteAuthorAsync(int id);
    }
}