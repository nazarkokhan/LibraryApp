﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;

        public AdminService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetUsersPageAsync(string? search, int page = 1, int items = 5)
        {
            return await _userManager.Users
                .Skip((page - 1) * items)
                .Take(items)
                .Where(u => u.UserName.Contains(search))
                .ToListAsync();
        }

        public async Task<User> GetUserAsync([Range(0, int.MaxValue)] int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> EditUserAsync(EditUserDto userDto)
        {
            var userEntity = await _userManager.FindByEmailAsync(userDto.CurrentEmail);

            userEntity.Email = userDto.NewEmail;

            userEntity.UserName = userDto.NewEmail;

            userEntity.Age = userDto.NewAge;

            await _userManager.ChangePasswordAsync(userEntity, userDto.CurrentPassword, userDto.NewPassword);

            return userEntity;
        }

        public async Task DeleteUserAsync([Range(0, int.MaxValue)] int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            await _userManager.DeleteAsync(user);
        }
    }
}