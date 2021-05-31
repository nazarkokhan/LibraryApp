using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibContext _db;

        public UserRepository(LibContext db)
        {
            _db = db;
        }

        public async Task<Pager<GetUserDto>> GetUsersPageAsync(int page, int itemsOnPage, string? search)
        {
            var totalCount = await _db.Users.CountAsync();

            var users = await _db.Users
                .Skip((page - 1) * itemsOnPage)
                .Take(itemsOnPage)
                .Select(u => new GetUserDto()
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password
                }).ToListAsync();

            return new Pager<GetUserDto>(users, totalCount);
        }

        public async Task<GetUserDto> GetUserAsync(int id)
        {
            return await _db.Users.Select(u => new GetUserDto
            {
                Id = u.Id,
                Login = u.Login,
                Password = u.Login
            }).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<GetUserDto> CreateUserAsync(CreateUserDto user)
        {
            var userEntity = new User
            {
                Login = user.Login,
                Password = user.Password
            };

            await _db.Users.AddAsync(userEntity);

            await _db.SaveChangesAsync();

            return new GetUserDto
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                Password = userEntity.Password
            };
        }

        public async Task<GetUserDto> UpdateUserAsync(UpdateUserDto user)
        {
            var authorEntity = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            authorEntity.Login = user.Login;

            authorEntity.Password = user.Password;

            await _db.SaveChangesAsync();

            return new GetUserDto
            {
                Id = authorEntity.Id,
                Login= authorEntity.Login,
                Password = authorEntity.Password
            };
        }

        public async Task DeleteUserAsync(int id)
        {
            var userEntity = await _db.Users
                .FirstOrDefaultAsync(a => a.Id == id);

            _db.Users.Remove(userEntity);

            await _db.SaveChangesAsync();
        }
    }
}