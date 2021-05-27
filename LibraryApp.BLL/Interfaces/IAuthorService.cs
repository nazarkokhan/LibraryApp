using System.Threading.Tasks;
using LibraryApp.DAL.DTO;

namespace LibraryApp.BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page);

        Task<GetAuthorDto> GetAuthorAsync(int id);

        Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author);

        Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author);

        Task DeleteAuthorAsync(int id);
    }
}
