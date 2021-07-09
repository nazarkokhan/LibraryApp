using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Author;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
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

        public Task<Result<Pager<AuthorDto>>> GetAuthorsAsync(int page, int itemsOnPage, string? search) 
            => _unitOfWork.Authors.GetAuthorsAsync(page, itemsOnPage, search);

        public Task<Result<AuthorDto>> GetAuthorAsync(int id) 
            => _unitOfWork.Authors.GetAuthorAsync(id);

        public Task<Result<AuthorDto>> CreateAuthorAsync(CreateAuthorDto author) 
            => _unitOfWork.Authors.CreateAuthorAsync(author);

        public Task<Result<AuthorDto>> UpdateAuthorAsync(UpdateAuthorDto author) 
            => _unitOfWork.Authors.UpdateAuthorAsync(author);

        public Task<Result> DeleteAuthorAsync(int id) 
            => _unitOfWork.Authors.DeleteAuthorAsync(id);
    }
}