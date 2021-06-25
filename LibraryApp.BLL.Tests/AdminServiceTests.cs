using System.Threading.Tasks;
using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class AdminServiceTests
    {
        private readonly IAdminService _adminService;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IUserStore<User>> _userStoreMock = new Mock<IUserStore<User>>();

        public AdminServiceTests()
        {
            _userManagerMock =
                new Mock<UserManager<User>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);

            _adminService =
                new AdminService(_userManagerMock.Object, _unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData("newUser@gmail.com", 25, "newAccess", "user@gmail.com")]
        public async Task EditUserAsync_EditUserDto_SuccessEditedUserReturned(
            string newEmail, int newAge, string newPassword, string id)
        {
            var editUserDto = new EditUserDto(newEmail, newAge, newPassword, id);

            var userEntity = new User
            {
                Email = newEmail,
                Age = newAge
            };
            
            _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Users
                .EditUserAsync(editUserDto)
            ).Returns(Task.FromResult(It.IsAny<Result<User>>()));
        
            _userManagerMock.Setup(userManager => userManager
                .RemovePasswordAsync(userEntity)
            ).Returns(Task.FromResult(IdentityResult.Success));
            
            _userManagerMock.Setup(userManager => userManager
                .AddPasswordAsync(userEntity, editUserDto.NewPassword)
            ).Returns(Task.FromResult(IdentityResult.Success));
        
            var actual = await _adminService.EditUserAsync(editUserDto);
        
            var expected = Result<User>.CreateSuccess(userEntity);
        
            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
        }
    }
}