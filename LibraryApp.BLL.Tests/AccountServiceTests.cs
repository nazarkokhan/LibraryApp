using System;
using System.Collections.Generic;
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
        private readonly Mock<UserManager<User>> _userManager;
        private readonly Mock<IEmailService> _emailService;
        
        public AccountServiceTests()
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            
            _userManager = new Mock<UserManager<User>>();
            _emailService = new Mock<IEmailService>();
            _accountService = new AccountService(mgr.Object, _emailService.Object);
        }

        [Fact]
        public void GetProfile_allIds_successReturned()
        {
            // var expected = _accountService.Setup(service => service.GetProfile()).Returns(Result<UserFromTokenDto>
            //         .CreateSuccess(new UserFromTokenDto(1, "admin@gmail.com", Roles.Admin)));
            
            var actual = _accountService.GetProfile(1);
            
            //expected.Equals(actual);
            // Assert.Equal(expected, actual);
        }

        // [Theory]
        // [InlineData(0)]
        // public void 

        // public static IEnumerable<object[]> TestData()
        // {
        //     yield return new object[] {new RegisterDto("ihorployka2@gmail.com", "IhorPassword", 20)};
        //     yield return new object[] {new RegisterDto("abek1ksd3dn@gmail.com", "NotExist", 20)};
        //     yield return new object[] {new RegisterDto("nazarkokhan@gmail.com", "NazarPassword", 20)};
        // }
    }
    
    
}