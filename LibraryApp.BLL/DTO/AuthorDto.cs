namespace LibraryApp.BLL.DTO
{
    public class GetAuthorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateAuthorDto
    {
        public string Name { get; set; }
    }

    public class PutAuthorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
