﻿namespace _04.RoutePrefix
{
    public class Book
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public string Title { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}