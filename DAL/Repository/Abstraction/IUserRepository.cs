using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<Result<bool>> UserExistsAsync(string email);

        Task<Result<Pager<User>>> GetUsersPageAsync(string? search, int page, int items);

        Task<Result<int>> CountUsersAsync();

        Task<Result<List<User>>> TakeUsersPageAsync(string? search, int page, int items);

        Task<Result<User>> FindUserAsync(int id);
    }
}