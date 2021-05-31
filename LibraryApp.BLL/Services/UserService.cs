using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Pager<GetUserDto>> GetUsersPageAsync(int page, int itemsOnPage, string? search)
        {
            return _unitOfWork.Users.GetUsersPageAsync(page, itemsOnPage, search);
        }

        public Task<GetUserDto> GetUserAsync(int id)
        {
            return _unitOfWork.Users.GetUserAsync(id);
        }

        public Task<GetUserDto> CreateUserAsync(CreateUserDto user)
        {
            return _unitOfWork.Users.CreateUserAsync(user);
        }

        public Task<GetUserDto> UpdateUserAsync(UpdateUserDto user)
        {
            return _unitOfWork.Users.UpdateUserAsync(user);
        }

        public Task DeleteUserAsync(int id)
        {
            return _unitOfWork.Users.DeleteUserAsync(id);
        }
    }
}