using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class GetAuthorDto
    {
        public GetAuthorDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }

    public class CreateAuthorDto
    {
        public CreateAuthorDto(string name)
        {
            Name = name;
        }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; } // TODO: all DTO properties shouldn't have set accessor
    }

    public class UpdateAuthorDto
    {
        public UpdateAuthorDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Required] 
        [Range(0, int.MaxValue)] 
        public int Id { get; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; }
    }
}