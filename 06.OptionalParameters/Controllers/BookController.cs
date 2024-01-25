using Microsoft.AspNetCore.Mvc;

namespace _06.OptionalParameters.Controllers
{
    [Route("book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [Route("books/id/{id:int?}")]
        [HttpGet]
        public IEnumerable<Book> GetBooksById(int id = 1)
        {
            IEnumerable<Book> books = new List<Book>()
            {
                new() { Id = 1, Author = "John Doe", Title = "Book 1" },
                new() { Id = 1, Author = "Jane Doe", Title = "Book 2" },
                new() { Id = 2, Author = "John Smith", Title = "Book 3" },
                new() { Id = 2, Author = "Jane Smith", Title = "Book 4" },
                new() { Id = 1, Author = "John Doe", Title = "Book 5" },
            };

            return books.Where(b => b.Id == id);
        }

        [Route("books/price/{price:int=10}")]
        [HttpGet]
        public IEnumerable<Book> GetBooksByName(decimal price)
        {
            IEnumerable<Book> books = new List<Book>()
            {
                new() { Id = 1, Author = "John Doe", Title = "Book 1", Price = 10},
                new() { Id = 1, Author = "Jane Doe", Title = "Book 2"   , Price = 10},
                new() { Id = 2, Author = "John Smith", Title = "Book 3" , Price = 15},
                new() { Id = 2, Author = "Jane Smith", Title = "Book 4" , Price = 15},
                new() { Id = 1, Author = "John Doe", Title = "Book 5"   , Price = 5},
            };

            return books.Where(b => b.Price == price);
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
    }
}
