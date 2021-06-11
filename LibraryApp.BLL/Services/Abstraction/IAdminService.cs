using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAdminService
    {
        Task<Result<Pager<User>>> GetUsersPageAsync(string? search, int page, int items);

        Task<Result<User>> GetUserAsync([Range(0, int.MaxValue)] int id);

        Task<Result<User>> EditUserAsync(EditUserDto userDto);

        Task<Result> DeleteUserAsync([Range(0, int.MaxValue)] int id);
    }
}