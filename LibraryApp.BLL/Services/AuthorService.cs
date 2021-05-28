using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int itemsOnPage)
        {
            return _unitOfWork.Authors.GetAuthorsAsync(page, itemsOnPage);
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
