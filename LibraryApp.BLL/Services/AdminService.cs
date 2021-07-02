using System;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultConstants;
using LibraryApp.Core.ResultConstants.AuthorizationConstants;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;

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

        public async Task<Result<User>> GetUserAsync(int id)
        {
            return await _unitOfWork.Users.GetUserAsync(id);
        }

        public async Task<Result<User>> EditUserAsync(EditUserDto editUserDto)
        {
            try
            {
                var editUserResult = await _unitOfWork.Users.EditUserAsync(editUserDto);

                if (!editUserResult.Success)
                    return editUserResult;

                var userEntity = editUserResult.Data;
                
                var removePassword = await _userManager.RemovePasswordAsync(userEntity);

                if(!removePassword.Succeeded)
                    return Result<User>.CreateFailed(AccountResultConstants.ErrorRemovingPassword);
                
                var addPass = await _userManager.AddPasswordAsync(userEntity, editUserDto.NewPassword);

                return !addPass.Succeeded
                    ? Result<User>.CreateFailed(AccountResultConstants.ErrorAddingPassword)
                    : Result<User>.CreateSuccess(userEntity);
            }
            catch (Exception e)
            {
                return Result<User>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result> DeleteUserAsync(int id)
        {
            return await _unitOfWork.Users.DeleteUserAsync(id);
        }
    }
}