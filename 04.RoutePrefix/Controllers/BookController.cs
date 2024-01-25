using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _04.RoutePrefix.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // GET /api/authors/1/books
        [Route("~/api/authors/{authorId:int}/books")]
        [HttpGet]
        public IEnumerable<Book> GetByAuthor(int authorId) 
        {
            return new List<Book>()
            {
                new Book() { Id = 1, Title = "Book 1", Author = new Author(){ Id = 1,  Name = "Author 1"} },
                new Book() { Id = 2, Title = "Book 2", Author = new Author(){ Id = 2,  Name = "Author 2"} },
                new Book() { Id = 3, Title = "Book 3", Author = new Author(){ Id = 3,  Name = "Author 3"} },
                new Book() { Id = 4, Title = "Book 4", Author = new Author(){ Id = 1,  Name = "Author 1"} }
            }.Where(p => p.Author.Id == authorId);
        }
    }
}
