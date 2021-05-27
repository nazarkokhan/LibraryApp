using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApp.DAL.DTO;
using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Pager<GetAuthorDto>> GetAuthorsAsync(int page);

        Task<GetAuthorDto> GetAuthorAsync(int id);

        Task<GetAuthorDto> CreateAuthorAsync(CreateAuthorDto author);

        Task<GetAuthorDto> UpdateAuthorAsync(UpdateAuthorDto author);

        Task DeleteAuthorAsync(int id);
    }
}