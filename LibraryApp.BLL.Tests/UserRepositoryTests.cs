﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LibraryApp.BLL.Tests
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepository;

        private readonly List<User> _dbSeeds;

        public UserRepositoryTests()
        {
            _dbSeeds = new List<User>
            {
                new() {Email = "admin@gmail.com", Age = 20},
                new() {Email = "user@gmail.com", Age = 25}
            };

            var dbContextOptions = new DbContextOptionsBuilder<LibContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var db = new LibContext(dbContextOptions);

            db.Users.AddRangeAsync(_dbSeeds);

            _userRepository = new UserRepository(db);
        }

        [Theory]
        [InlineData("user")]
        [InlineData("admin", 1, 3)]
        [InlineData("user", 2, 1)]
        public async Task GetUsersPageAsync_SearchAndPageAndItems_SuccessPageOfUsersReturned(
            string search, int page = 1, int items = 5)
        {
            var actual = await _userRepository.GetUsersPageAsync(search, page, items);

            var expected = Result<Pager<User>>.CreateSuccess(It.IsAny<Pager<User>>());

            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
        }
        
        [Theory]
        [InlineData("user")]
        [InlineData("admin", 1, 3)]
        [InlineData("user", 2, 1)]
        public async Task GetUsersPageAsync_SearchAndPageAndItems_FailUnexpectedReturned(
            string search, int page = 1, int items = 5)
        {
            var actual = await _userRepository.GetUsersPageAsync(search, page, items);

            //Wrong expectation
            var expected = Result<Pager<User>>.CreateSuccess(It.IsAny<Pager<User>>());
            
            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUserAsync_Id_SuccessUserReturned(int id)
        {
            var actual = await _userRepository.GetUserAsync(id);

            var expected = Result<User>.CreateSuccess(actual.Data);

            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
            Assert.Equal(expected.Data, actual.Data);
        }
        
        [Theory]
        [InlineData(83)]
        [InlineData(999)]
        public async Task GetUserAsync_Id_FailUserNotFoundReturned(int id)
        {
            var actual = await _userRepository.GetUserAsync(id);

            var expected = Result<User>.CreateFailed(
                AccountResultConstants.UserNotFound,
                new NullReferenceException()
            );

            Assert.NotNull(actual);
            Assert.NotNull(actual.Exception);
            Assert.False(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
        }
        
        [Theory]
        [InlineData("newAdmin@gmail.com", 50, "newAccess", 1)]
        [InlineData("newUser@gmail.com", 10, "newAccess", 2)]
        public async Task GetUserAsync_Id_SuccessEditedUserReturned(
            string newEmail, int newAge, string newPassword, int id)
        {
            var userDto = new EditUserDto(newEmail, newAge, newPassword, id);

            var actual = await _userRepository.EditUserAsync(userDto);

            var expected = Result<User>.CreateSuccess(actual.Data);

            Assert.NotNull(actual);
            Assert.Null(actual.Exception);
            Assert.True(actual.Success);
            Assert.Equal(expected.Success, actual.Success);
            Assert.Equal(expected.Data, actual.Data);
        }
    }
}