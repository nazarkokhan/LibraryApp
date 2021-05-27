using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.DTO;
using LibraryApp.DAL.Repository;

namespace LibraryApp.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly EFUnitOfWork _unitOfWork;

        public AuthorService(EFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page)
        {
            return _unitOfWork.Authors.GetAuthorsAsync(page);
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
