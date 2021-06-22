using System.Threading.Tasks;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.DAL.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<Result<bool>> UserExistsAsync(string email);
    }
}