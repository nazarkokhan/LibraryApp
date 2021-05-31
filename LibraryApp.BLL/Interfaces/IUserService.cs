using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<Pager<GetUserDto>> GetUsersPageAsync(int page, int itemsOnPage, string? search);

        Task<GetUserDto> GetUserAsync(int id);

        Task<GetUserDto> CreateUserAsync(CreateUserDto user);

        Task<GetUserDto> UpdateUserAsync(UpdateUserDto user);

        Task DeleteUserAsync(int id);
    }
}