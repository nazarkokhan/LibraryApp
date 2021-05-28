using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibContext _db;

        public AuthorRepository(LibContext context)
        {
            _db = context;
        }

        public async Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int itemsOnPage)
        {
            var totalCount = await _db.Authors.CountAsync();

            var authors = await _db.Authors
                .Skip((page - 1) * itemsOnPage)
                .Take(itemsOnPage)
                .Select(a => new GetAuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToListAsync();

            return new Pager<GetAuthorDto>(authors, totalCount);
        }

        public async Task<GetAuthorDto> GetAuthorAsync(int id)
        {
            return await _db.Authors.Where(a => a.Id == id).Select(a => new GetAuthorDto
            {
                Id = a.Id,
                Name = a.Name,
            }).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author)
        {
            var authorEntity = new Author
            {
                Name = author.Name
            };

            await _db.Authors.AddAsync(authorEntity);

            await _db.SaveChangesAsync();

            return new GetAuthorDto
            {
                Id = authorEntity.Id,
                Name = authorEntity.Name
            };
        }

        public async Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            var authorEntity = await _db.Authors
                .Where(a => a.Id == author.Id)
                .FirstOrDefaultAsync();

            authorEntity.Name = author.Name;

            await _db.SaveChangesAsync();

            return new GetAuthorDto
            {
                Id = authorEntity.Id,
                Name = authorEntity.Name
            };
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var authorEntity = await _db.Authors
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            _db.Authors.Remove(authorEntity);

            await _db.SaveChangesAsync();
        }
    }
}