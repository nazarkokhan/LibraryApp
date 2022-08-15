namespace LibraryApp.Core.DTO.Author;

using System.ComponentModel.DataAnnotations;

public class CreateAuthorDto
{
    public CreateAuthorDto(string name)
    {
        Name = name;
    }

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string Name { get; }
}