using System;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.DAL.Repository
{
    class EFUnitOfWork : IUnitOfWork
    {
        private LibContext _db;

        private AuthorRepository _authorRepository;

        private BookRepository _bookRepository;

        private bool _disposed = false;

        public EFUnitOfWork(LibContext context)
        {
            _db = context;
        }

        public IRepository<Author> Authors
        {
            get
            {
                if (_authorRepository == null)
                {
                    _bookRepository = new BookRepository(_db);
                }

                return _authorRepository;
            }
        }

        public IRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_db);
                }

                return _bookRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
