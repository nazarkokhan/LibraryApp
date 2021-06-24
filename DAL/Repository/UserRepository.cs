using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.Extensions;
using LibraryApp.Core.ResultConstants;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibContext _db;

        public UserRepository(LibContext context)
        {
            _db = context;
        }

        public async Task<Result<bool>> UserExistsAsync(string email)
        {
            try
            {
                return Result<bool>.CreateSuccess(await _db.Users.AnyAsync(u => u.Email == email));
            }
            catch (Exception e)
            {
                return Result<bool>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result<Pager<User>>> GetUsersPageAsync(string? search, int page, int items)
        {
            try
            {
                var totalCount = await _db.Users
                    .CountAsync();
                
                var userEntities = _db.Users
                    .OrderBy(a => a.Id)
                    .TakePage(page, items);

                if (!string.IsNullOrWhiteSpace(search))
                    userEntities = userEntities
                        .Where(u => u.UserName.Contains(search) || u.Email.Contains(search));

                return Result<Pager<User>>.CreateSuccess(
                    new Pager<User>(
                        await userEntities.ToListAsync(),
                        totalCount
                    )
                );
            }
            catch (Exception e)
            {
                return Result<Pager<User>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
        public async Task<Result<User>> EditUserAsync(EditUserDto userDto)
        {
            try
            {
                var userEntity = await _db.Users
                    .FirstOrDefaultAsync(u => u.Email == userDto.CurrentEmail);

                if (userEntity is null)
                    return Result<User>.CreateFailed("User does`nt exist", new NullReferenceException());

                userEntity.Email = userDto.NewEmail;

                userEntity.UserName = userDto.NewEmail;

                userEntity.Age = userDto.NewAge;

                // await _userManager.RemovePasswordAsync(userEntity);

                // var addPass = await _userManager.AddPasswordAsync(userEntity, userDto.NewPassword);

                // return !addPass.Succeeded
                //     ? Result<User>.CreateFailed("Error adding new password")
                //     : Result<User>.CreateSuccess(userEntity);
                return null;
            }
            catch (Exception e)
            {
                return Result<User>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<Result<List<User>>> TakeUsersPageAsync(string? search, int page, int items)
        {
            try
            {
                var userEntities = _db.Users
                    .OrderBy(a => a.Id)
                    .TakePage(page, items);

                if (!string.IsNullOrWhiteSpace(search))
                    userEntities = userEntities
                        .Where(u => u.UserName.Contains(search) || u.Email.Contains(search));

                return Result<List<User>>.CreateSuccess(
                    await userEntities.ToListAsync()
                );
            }
            catch (Exception e)
            {
                return Result<List<User>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
        
        public async Task<Result<User>> FindUserAsync(int id)
        {
            try
            {
                return Result<User>.CreateSuccess(await _db.Users.FirstOrDefaultAsync(u => u.Id == id));
            }
            catch (Exception e)
            {
                return Result<User>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}