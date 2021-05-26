using System.Collections.Generic;
using System.Linq;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;

namespace LibraryApp.DAL.Repository
{
    class AuthorRepository : IRepository<Author>
    {
        private LibContext _db;

        public AuthorRepository(LibContext context)
        {
            _db = context;
        }


        public IEnumerable<Author> GetAll()
        {
            return _db.Authors;
        }

        public Author Get(int id)
        {
            return _db.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Author item)
        {
            _db.Authors.Add(item);
        }

        public void Update(Author item)
        {
            _db.Authors.Update(item);
        }

        public void Delete(int id)
        {
            var authorEntity = _db.Authors.FirstOrDefault(a => a.Id == id);
            if (authorEntity != null)
            {
                _db.Remove(authorEntity);
            }
        }
    }
}
