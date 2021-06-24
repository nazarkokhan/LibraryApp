using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class AdminServiceTests
    {
        private readonly IAdminService _adminService;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IUserStore<User>> _userStoreMock = new Mock<IUserStore<User>>();

        public AdminServiceTests()
        {
            _userManagerMock =
                new Mock<UserManager<User>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);

            _adminService =
                new AdminService(_userManagerMock.Object, _unitOfWork.Object);
        }

        [Theory]
        // [InlineData(null)]
        // [InlineData("sk", 5)]
        [InlineData("User", 2, 3)]
        public async Task GetUsersPageAsync_SearchAndPageAndItems_SuccessPageOfUsersReturned(
            string search, int page = 1, int items = 5)
        {
            _unitOfWork.Setup(unitOfWork => unitOfWork.Users
                .CountUsersAsync()
            ).Returns(Task.FromResult(It.IsAny<Result<int>>()));

            _unitOfWork.Setup(unitOfWork => unitOfWork.Users
                .TakeUsersPageAsync(search, page, items)
            ).Returns(Task.FromResult(It.IsAny<Result<List<User>>>()));

            var actual = await _adminService.GetUsersPageAsync(search, page, items);

            var expected = Result<Pager<User>>.CreateSuccess(
                new Pager<User>(
                    It.IsAny<List<User>>(),
                    It.IsAny<int>()
                )
            );

            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
        }
        
        // [Theory]
        // // [InlineData(null)]
        // // [InlineData("sk", 5)]
        // [InlineData("User", 2, 3)]
        // public async Task GetUserAsync_Id_SuccessUserReturned(int id)
        // {
        //     _unitOfWork.Setup(unitOfWork => unitOfWork.Users
        //         .CountUsersAsync()
        //     ).Returns(Task.FromResult(It.IsAny<Result<int>>()));
        //
        //     _unitOfWork.Setup(unitOfWork => unitOfWork.Users
        //         .TakeUsersPageAsync(search, page, items)
        //     ).Returns(Task.FromResult(It.IsAny<Result<List<User>>>()));
        //
        //     var actual = await _adminService.GetUserAsync(search, page, items);
        //
        //     var expected = Result<Pager<User>>.CreateSuccess(
        //         new Pager<User>(
        //             It.IsAny<List<User>>(),
        //             It.IsAny<int>()
        //         )
        //     );
        //
        //     Assert.NotNull(actual);
        //     Assert.Null(actual.Exception);
        //     Assert.True(actual.Success);
        //     Assert.Equal(expected.Success, actual.Success);
        // }
    }
}