using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository.Abstraction;
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

        public async Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page, int items, string? search)
        {
            var totalCount = await _db.Authors.CountAsync();

            var noSearch = string.IsNullOrWhiteSpace(search);

            var authors = _db.Authors
                .OrderBy(a => a.Id)
                .TakePage(page, items)
                .Select(a => new GetAuthorDto(a.Id, a.Name));

            return noSearch ? new Pager<GetAuthorDto>(await authors.ToListAsync(), totalCount)
                : new Pager<GetAuthorDto>(await authors.Where(u => u.Name.Contains(search!)).ToListAsync(), totalCount);
        }

        public async Task<GetAuthorDto> GetAuthorAsync(int id)
        {
            var user = await _db.Authors
                .Select(a => new GetAuthorDto(a.Id, a.Name))
                .FirstOrDefaultAsync(a => a.Id == id);
            return user;
        }

        public async Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author)
        {
            var authorEntity = new Author
            {
                Name = author.Name
            };

            await _db.Authors.AddAsync(authorEntity);

            await _db.SaveChangesAsync();

            return new GetAuthorDto(authorEntity.Id, authorEntity.Name);
        }

        public async Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author)
        {
            var authorEntity = await _db.Authors
                .FirstOrDefaultAsync(a => a.Id == author.Id); // TODO: entity can be null

            authorEntity.Name = author.Name;

            await _db.SaveChangesAsync();

            return new GetAuthorDto(authorEntity.Id, authorEntity.Name);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var authorEntity = await _db.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            _db.Authors.Remove(authorEntity); // TODO: entity can be null

            await _db.SaveChangesAsync();
        }
    }
}