using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ResultConstants;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibContext _db;

        public UserRepository(LibContext context)
        {
            _db = context;
        }
        public async Task<Result<bool>> UserExistsAsync(string email)
        {
            try
            {
                return Result<bool>.CreateSuccess(await _db.Users.AnyAsync(u => u.Email == email));
            }
            catch (Exception e)
            {
                return Result<bool>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}