﻿namespace LibraryApp.DAL.Entities
{
    public class AuthorBook
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Author Author { get; set; }

        public int AuthorId { get; set; }
        public Book Book { get; set; }
    }
}