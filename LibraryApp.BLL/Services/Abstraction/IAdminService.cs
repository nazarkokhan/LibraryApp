using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetUsersPageAsync(string? search, int page = 1, int items = 5);

        Task<User> GetUserAsync([Range(0, int.MaxValue)] int id);

        Task<User> EditUserAsync(EditUserDto userDto);

        Task DeleteUserAsync([Range(0, int.MaxValue)] int id);
    }
}