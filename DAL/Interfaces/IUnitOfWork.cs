using System.Threading.Tasks;

namespace LibraryApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }

        IBookRepository Books { get; }

        Task SaveAsync();
    }
}
