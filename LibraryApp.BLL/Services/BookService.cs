using System.Threading.Tasks;
using LibraryApp.BLL.Interfaces;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Pager<GetBookDto>> GetBooksAsync(int page, int itemsOnPage)
        {
            return _unitOfWork.Books.GetBooksAsync(page, itemsOnPage);
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
