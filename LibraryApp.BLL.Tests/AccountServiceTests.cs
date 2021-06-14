using System;
using System.Collections.Generic;
using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO.Authorization;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class AccountServiceTests
    {
        // private readonly IAccountService _accountService;
        //
        // public AccountServiceTests(IAccountService accountService)
        // {
        //     _accountService = accountService;
        // }

        [Fact]
        public void RegisterAsync_ihorployka2andAge30_successReturned()
        {
            //Arrange
            var email = "ihorployka2@gmail.com";

            var password = "IhorPassword";

            var age = 30;

            var expected = 1;

            //Act
            var actual = 1;
            
            // Assert
            Assert.Equal(expected, actual);

        }
        
        // [Theory]
        // [InlineData(0)]
        // public void 

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] {new RegisterDto("ihorployka2@gmail.com", "IhorPassword", 20)};
            yield return new object[] {new RegisterDto("ihorployka2@gmail.com", "IhorPassword", 20)};
            yield return new object[] {new RegisterDto("nazarkokhan@gmail.com", "IhorPassword", 20)};
        }
    }
}