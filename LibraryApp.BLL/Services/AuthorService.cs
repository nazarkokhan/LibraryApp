using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Repository.Abstraction;

namespace LibraryApp.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int itemsOnPage, string? search)
        {
            return _unitOfWork.Authors.GetAuthorsAsync(page, itemsOnPage, search);
        }

        public Task<GetAuthorDto> GetAuthorAsync(int id)
        {
            return _unitOfWork.Authors.GetAuthorAsync(id);
        }

        public Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author)
        {
            return _unitOfWork.Authors.CreateAuthorAsync(author);
        }

        public Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            return _unitOfWork.Authors.UpdateAuthorAsync(author);
        }

        public Task DeleteAuthorAsync(int id)
        {
            return _unitOfWork.Authors.DeleteAuthorAsync(id);
        }
    }
}