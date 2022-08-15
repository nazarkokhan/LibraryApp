using System.Collections.Generic;
using System.Runtime.Serialization;
using LibraryApp.Core.DTO.Author;

namespace LibraryApp.Core.DTO.Book;

public class BookDto
{
    public BookDto(int id, string name, IEnumerable<AuthorDto> authors)
    {
        Id = id;
        Name = name;
        Authors = authors;
    }
    public int Id { get; }

    public string Name { get; }

    [DataMember]
    public IEnumerable<AuthorDto> Authors { get; }
}