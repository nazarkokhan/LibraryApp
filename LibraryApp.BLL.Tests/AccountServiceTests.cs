using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class AccountServiceTests
    {
        private readonly IAccountService _accountService;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IEmailService> _emailService;
        private readonly RegisterDto _registerDto = new RegisterDto("ihorployka2@gmail.com", "ihorAccess", 30);
        public AccountServiceTests()
        {
            var store = new Mock<IUserStore<User>>();
            
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _emailService = new Mock<IEmailService>();
            _accountService = new AccountService(_userManagerMock.Object, _emailService.Object);
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
            
            Assert.Equal(actual.Data.Id, expected.Id);
            Assert.Equal(actual.Data.Email, expected.Email);
        }

        [Theory]
        [ClassData()]
        public async Task SendRegisterToken_RegisterDto_successReturned(RegisterDto userDto)
        {
            var x = await _accountService.SendRegisterTokenAsync() 
        }
        
    }
    
}