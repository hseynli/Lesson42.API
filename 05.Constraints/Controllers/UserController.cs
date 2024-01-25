using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _05.Constraints.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("users/{id:int:min(1)}")]
        [HttpGet]
        public User GetUserById(int id) 
        {
            IEnumerable<User> users = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" },
                new User { Id = 3, Name = "Jack" }
            };

            return users.FirstOrDefault(u => u.Id == id);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
