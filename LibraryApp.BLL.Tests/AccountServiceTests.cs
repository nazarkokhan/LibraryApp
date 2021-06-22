using System;
using System.Threading.Tasks;
using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class AccountServiceTests
    {
        private readonly IAccountService _accountService;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IAccountService> _accountServiceMock;

        public AccountServiceTests()
        {
            var store = new Mock<IUserStore<User>>();

            _userManagerMock =
                new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            
            _emailServiceMock = new Mock<IEmailService>();
            
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            
            _accountService = 
                new AccountService(_userManagerMock.Object, _emailServiceMock.Object, _unitOfWorkMock.Object);
            
            _accountServiceMock = new Mock<IAccountService>();
        }

        [Theory]
        [InlineData(1, "admin@gmail.com")]
        [InlineData(2, "admin1@gmail.com")]
        [InlineData(3, "admin2@gmail.com")]
        public async Task GetProfile_IdFromClaims_successReturned(int id, string email)
        {
            var expected = new ProfileDto(id, email);

            _userManagerMock
                .Setup(manager => manager.FindByIdAsync(id.ToString()))
                .Returns(Task.FromResult(new User
                {
                    Id = id,
                    Email = email
                }));


            var actual = await _accountService.GetProfile(id);

            Assert.NotNull(actual);
            Assert.NotNull(actual.Data);
            Assert.True(actual.Success);
            Assert.Equal(actual.Data.Id, expected.Id);
            Assert.Equal(actual.Data.Email, expected.Email);
        }
        
        [Theory]
        [InlineData(1, "admin@gmail.com")]
        [InlineData(2, "admin1@gmail.com")]
        [InlineData(3, "admin2@gmail.com")]
        public async Task GetProfile_IdFromClaims_failedReturned(int id, string email)
        {
            var expected = new ProfileDto(id, email);

            _userManagerMock
                .Setup(manager => manager.FindByIdAsync(id.ToString()))
                .Throws(new Exception());
            
            
            var actual = await _accountService.GetProfile(id);
            
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            // Assert.Equal(actual.Data.Id, expected.Id);
            // Assert.Equal(actual.Data.Email, expected.Email);
        }

        [Theory]
        [InlineData("nazarkokhan@gmail.com", "nazarAccess", 25)]
        [InlineData("ihorployka2@gmail.com", "ihorAccess", 30)]
        [InlineData("ihorployka8@gmail.com", "ihorAccess", 35)]
        public async Task CreateUserAndSendEmailToken_RegisterDto_successReturned(string regEmail, string regPassword, int regAge)
        {
            var register = new RegisterDto(regEmail, regPassword, regAge);

            var userEntity = new User
            {
                Email = register.Email,
                UserName = register.Email,
                Age = register.Age
            };

            var generatedToken = Task.FromResult(
                "CfDJ8LNyxVtZsTpInvOIHbQXaGmEYG4znYzq8Ki0JXMRr2GeKHdaE9xpUgp87gtf+MCiXoXoyy" +
                "iomn+2CIlwPyr16mHeix3M8mcO9uOJ9dN9G9aim4JsTk8KAsHZoUI//Hd0BjU0Q4GyBu3/wJm" +
                "lfeOUlbCfvxgxBDdDM/v/aX0ahrYeOzTRv6IrD2CWSftsledu8aVz3ZJGXc2Ks6QcoaO7+Ko="
            );
            
            var expected = Task.CompletedTask;

            _userManagerMock.Setup(usManager => usManager
                .CreateAsync(userEntity, register.Password)
            ).Returns(Task.FromResult(IdentityResult.Success));
            
            _userManagerMock.Setup(usManager => usManager
                .GenerateEmailConfirmationTokenAsync(new User
                {
                    Email = register.Email, UserName = register.Email, Age = register.Age
                })
            ).Returns(generatedToken);


            _emailServiceMock.Setup(emService => emService
                .SendAsync(
                    register.Email,
                    default,
                    AccountEmailServiceConstants.ConfirmRegistration
                )
            ).Returns(Task.CompletedTask);

            var actual = await _accountService.CreateUserAndSendEmailTokenAsync(register);

            // Assert.Equal(actual.Success, default);
        }

        // [Theory]
        // [InlineData("nazarkokhan@gmail.com", "token", "1")]
        // [InlineData("ihorployka2@gmail.com", "token", "2")]
        // [InlineData("ihorployka8@gmail.com", "token", "3")]
        // public async Task ConfirmRegistration_TokenAndUserId_successReturned(string expected, string token,
        //     string userId)
        // {
        //     var userEntity = Task.FromResult(new User
        //     {
        //         Id = int.Parse(userId),
        //         Email = expected
        //     });
        //
        //     _userManagerMock
        //         .Setup(manager => manager.FindByIdAsync(userId))
        //         .Returns(userEntity);
        //
        //     // _userManagerMock
        //     //     .Setup(manager => manager.UpdateAsync(userEntity.Result))
        //     //     .Returns()
        //     //     
        //     // _userManagerMock
        //     //     .Setup(manager => manager.ConfirmEmailAsync(userEntity.Result, token))
        //     //     .Returns(userEntity.Result.EmailConfirmed == true)
        // }
    }
}