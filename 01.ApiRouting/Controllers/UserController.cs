using _01.ApiRouting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01.ApiRouting.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get");
        }

        [HttpGet("{name}/{surname}")]
        public IActionResult Get(string name, string surname)
        {
            return Ok($"Hello, {name} {surname}");
        }

        [HttpGet("hello/{name}")]
        public IActionResult Get(string name)
        {
            return Ok($"Hello, {name}");
        }        
    }
}
