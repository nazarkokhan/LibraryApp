using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAuthorService
    {
        Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int itemsOnPage);

        Task<GetAuthorDto> GetAuthorAsync(int id);

        Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author);

        Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author);

        Task DeleteAuthorAsync(int id);
    }
}
