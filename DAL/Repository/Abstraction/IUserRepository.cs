using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Repository.Abstraction;

public interface IUserRepository
{
    Task<Result<Pager<User>>> GetUsersPageAsync(string? search, int page, int items);

    Task<Result<User>> GetUserAsync(int id);
        
    Task<Result<User>> EditUserAsync(EditUserDto userDto);

    Task<Result> DeleteUserAsync(int id);
        
    Task<Result<bool>> UserExistsAsync(string email);
}