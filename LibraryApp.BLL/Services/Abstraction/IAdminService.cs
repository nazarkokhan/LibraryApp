using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetUsersPage(string? search, int page = 1, int items = 5);

        Task<User> GetUser([Range(0, int.MaxValue)] int id);

        Task EditUser(EditUserDto userDto);

        Task DeleteUser([Range(0, int.MaxValue)] int id);
    }
}