using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.Extensions;
using LibraryApp.Core.ResultConstants;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        public AdminService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pager<User>>> GetUsersPageAsync(string? search, int page, int items)
        {
            return await _unitOfWork.Users.GetUsersPageAsync(search, page, items);
        }

        public async Task<Result<User>> GetUserAsync([Range(0, int.MaxValue)] int id)
        {
            try
            {
                var userEntity = await _unitOfWork.Users
                    .FindUserAsync(id);

                return userEntity?.Data is null
                    ? Result<User>.CreateFailed(
                        AccountResultConstants.UserNotFound,
                        new NullReferenceException()
                    )
                    : Result<User>.CreateSuccess(userEntity.Data);
            }
            catch (Exception e)
            {
                return Result<User>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result<User>> EditUserAsync(EditUserDto userDto)
        {
            try
            {
                var userEntity = await _userManager
                    .FindByEmailAsync(userDto.CurrentEmail);

                if (userEntity is null)
                    return Result<User>.CreateFailed("User does`nt exist", new NullReferenceException());

                userEntity.Email = userDto.NewEmail;

                userEntity.UserName = userDto.NewEmail;

                userEntity.Age = userDto.NewAge;

                await _userManager.RemovePasswordAsync(userEntity);

                var addPass = await _userManager.AddPasswordAsync(userEntity, userDto.NewPassword);

                return !addPass.Succeeded
                    ? Result<User>.CreateFailed("Error adding new password")
                    : Result<User>.CreateSuccess(userEntity);
            }
            catch (Exception e)
            {
                return Result<User>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> DeleteUserAsync([Range(0, int.MaxValue)] int id)
        {
            try
            {
                var userEntity = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (userEntity is null)
                    return Result.CreateFailed(
                        AccountResultConstants.UserNotFound,
                        new NullReferenceException()
                    );

                await _userManager.DeleteAsync(userEntity);

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}