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
    public class BookService : IBookService
    {
        private readonly EFUnitOfWork _unitOfWork;

        public BookService(EFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Pager<GetBookDto>> GetBooksAsync(int page)
        {
            return _unitOfWork.Books.GetBooksAsync(page);
        }

        public Task<GetBookDto> GetBookAsync(int id)
        {
            return _unitOfWork.Books.GetBookAsync(id);
        }

        public Task<GetBookDto> CreateBookAsync(CreateBookDto book)
        {
            return _unitOfWork.Books.CreateBookAsync(book);
        }

        public Task<GetBookDto> UpdateBookAsync(UpdateBookDto book)
        {
            return _unitOfWork.Books.UpdateBookAsync(book);
        }

        public Task DeleteBookAsync(int id)
        {
            return _unitOfWork.Books.DeleteBookAsync(id);
        }
    }
}
