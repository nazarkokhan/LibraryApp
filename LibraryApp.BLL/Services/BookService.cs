using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.Core.DTO;
using LibraryApp.Core.DTO.Book;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;
using LibraryApp.DAL.Repository.Abstraction;

namespace LibraryApp.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Pager<BookDto>>> GetBooksAsync(int page, int itemsOnPage, string? search) 
            => _unitOfWork.Books.GetBooksAsync(page, itemsOnPage, search);

        public Task<Result<BookDto>> GetBookAsync(int id) 
            => _unitOfWork.Books.GetBookAsync(id);

        public Task<Result<BookDto>> CreateBookAsync(CreateBookDto book) 
            => _unitOfWork.Books.CreateBookAsync(book);

        public Task<Result<BookDto>> UpdateBookAsync(UpdateBookDto book) 
            => _unitOfWork.Books.UpdateBookAsync(book);

        public Task<Result> DeleteBookAsync(int id) 
            => _unitOfWork.Books.DeleteBookAsync(id);
    }
}