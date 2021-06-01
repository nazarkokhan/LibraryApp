using System.Threading.Tasks;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.DAL.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly LibContext _db;
        
        public EfUnitOfWork(LibContext context, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _db = context;
            Authors = authorRepository;
            Books = bookRepository;
        }

        public IAuthorRepository Authors { get; }

        public IBookRepository Books { get; }

        public Task SaveAsync() => _db.SaveChangesAsync();
    }
}
