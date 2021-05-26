using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.DAL.Repository
{
    class BookRepository : IRepository<Book>
    {
        private LibContext _db;

        public BookRepository(LibContext context)
        {
            _db = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _db.Books;
        }

        public Book Get(int id)
        {
            return _db.Books.Include(b => b.AuthorBooks).FirstOrDefault(b => b.Id == id);
        }

        public void Create(Book item)
        {
            _db.Books.Add(item);
        }

        public void Update(Book item)
        {
            _db.Books.Update(item);
        }

        public void Delete(int id)
        {
            var bookEntity = _db.Books.FirstOrDefault(b => b.Id == id);
            if (bookEntity != null)
            {
                _db.Books.Remove(bookEntity);
            }
        }
    }
}
