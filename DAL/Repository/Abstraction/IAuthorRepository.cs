using System.Threading.Tasks;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.DAL.Repository.Abstraction
{
    public interface IAuthorRepository
    {
        Task<Result<Pager<AuthorDto>>> GetAuthorsAsync(int page, int items, string? search);

        Task<Result<AuthorDto>> GetAuthorAsync(int id);

        Task<Result<AuthorDto>> CreateAuthorAsync(CreateAuthorDto author);

        Task<Result<AuthorDto>> UpdateAuthorAsync(UpdateAuthorDto author);

        Task<Result> DeleteAuthorAsync(int id);
    }
}