using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthorController : ControllerBase
    {
        private readonly LibContext _db;
        public AuthorController(LibContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAuthorDto>>> GetAuthorsAsync()
        {
            return Ok(await _db.Authors.Select(a => new GetAuthorDto
            {
                Id = a.Id,
                Name = a.Name,
            }).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookDto>> GetAuthorAsync([Required][Range(0, int.MaxValue)] int id)
        {
            var result = await _db.Authors.Where(a => a.Id == id).Select(a => new GetAuthorDto
            {
                Id = a.Id,
                Name = a.Name,
            }).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GetAuthorDto>> CreateAuthorAsync(CreateAuthorDto author)
        {
            var authorEntity = new Author
            {
                Name = author.Name
            };

            await _db.AddAsync(authorEntity);

            await _db.SaveChangesAsync();

            return Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{HttpContext.Request.Path}{authorEntity.Id}",
                new GetAuthorDto()
                {
                    Id = authorEntity.Id,
                    Name = authorEntity.Name
                });
        }


        [HttpPut]
        public async Task<ActionResult<GetAuthorDto>> UpdateAuthorAsync(PutAuthorDto author)
        {
            var authorEntity = await _db.Authors
                .Where(a => a.Id == author.Id)
                .FirstOrDefaultAsync();

            if (authorEntity == null)
            {
                return NotFound();
            }

            authorEntity.Name = author.Name;

            await _db.SaveChangesAsync();

            return Ok(new GetAuthorDto
            {
                Id = authorEntity.Id,
                Name = authorEntity.Name
            });
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAuthorAsync([Required][Range(0, int.MaxValue)] int id)
        {
            var authorEntity = await _db.Authors
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (authorEntity == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(authorEntity);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
