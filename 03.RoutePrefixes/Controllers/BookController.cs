using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03.RoutePrefixes.Controllers
{
    [Route("book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet("")]
        public IEnumerable<Book> GetBooks() 
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" },
                new Book { Id = 3, Title = "Book 3" },
                new Book { Id = 4, Title = "Book 4" },
                new Book { Id = 5, Title = "Book 5" },
            };
        }

        [HttpGet]
        [Route("{id:int}")]
        public Book GetBook(int id)
        {
            IEnumerable<Book> _books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" },
                new Book { Id = 3, Title = "Book 3" },
                new Book { Id = 4, Title = "Book 4" },
                new Book { Id = 5, Title = "Book 5" },
            };

            return _books.FirstOrDefault(b => b.Id == id);
        }
    }

    public sealed class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
