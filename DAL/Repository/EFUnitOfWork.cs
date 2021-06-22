using System.Threading.Tasks;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Repository.Abstraction;

namespace LibraryApp.DAL.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly LibContext _db;

        public EfUnitOfWork(LibContext context, IAuthorRepository authorRepository, IBookRepository bookRepository, IUserRepository users)
        {
            _db = context;
            Authors = authorRepository;
            Books = bookRepository;
            Users = users;
        }

        public IAuthorRepository Authors { get; }

        public IBookRepository Books { get; }
        
        public IUserRepository Users { get; }

        public Task SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}