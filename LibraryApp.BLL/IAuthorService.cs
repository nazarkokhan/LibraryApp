using System.Collections;
using System.Collections.Generic;
using LibraryApp.BLL.DTO;

namespace LibraryApp.BLL
{
    interface IAuthorService
    {
        void AddAuthor(CreateAuthorDto createAuthorDto);

        GetAuthorDto GetAuthor(int id);

        IEnumerable<GetAuthorDto> GetAuthors();

        void Dispose();
    }
}
