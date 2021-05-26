using System;
using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Author> Authors { get; }

        IRepository<Book> Books { get; }

        void Save();
    }
}
