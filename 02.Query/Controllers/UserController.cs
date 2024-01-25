using _01.ApiRouting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _02.Query.Controllers
{
    [Route("user")]
    [ApiController]
    public class User2Controller : ControllerBase
    {
        [HttpGet("GetWithQuery")]
        public IActionResult GetWithQuery([FromQuery] Person person)
        {
            return Ok($"Hello, {person.Name} {person.Surname}");
        }

        [HttpGet]
        public IActionResult SayHello([FromQuery] string name)
        {
            return Ok($"Hello, {name}");
        }
    }
}
