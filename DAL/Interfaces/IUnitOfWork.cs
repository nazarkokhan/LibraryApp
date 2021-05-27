using System;
using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }

        IBookRepository Books { get; }

        void Save();
    }
}
