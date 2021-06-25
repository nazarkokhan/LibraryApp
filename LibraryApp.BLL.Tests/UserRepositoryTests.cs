using System.Threading.Tasks;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Repository;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class UserRepositoryTests
    {
        private readonly LibContext _db;
        private IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            // var dbContextOptions = new DbContextOptionsBuilder<LibContext>()
            //     .UseInMemoryDatabase("TestDb")
            //     .Options;
            //
            // _db = new LibContext(dbContextOptions);
            //
            // _userRepository = new UserRepository(_db);
        }

        private IUserRepository GetLibraryRepository()
        {
            var dbContextOptions = new DbContextOptionsBuilder<LibContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            
            var context = new LibContext(dbContextOptions);
            
            var repository = new UserRepository(_db);

            return repository;
        }
        [Theory]
        [InlineData("User")]
        public async Task GetUsersPageAsync_SearchAndPageAndItems_SuccessPageOfUsersReturned(
            string search, int page = 1, int items = 5)
        {
            _userRepository = GetLibraryRepository();
            
            var actual = await _userRepository.GetUsersPageAsync(search, page, items);
            
            Assert.NotNull(actual);
        }
    }
}