using System.Threading.Tasks;

namespace LibraryApp.DAL.Repository.Abstraction;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }

    IBookRepository Books { get; }
        
    IUserRepository Users { get; }

    Task SaveAsync();
}