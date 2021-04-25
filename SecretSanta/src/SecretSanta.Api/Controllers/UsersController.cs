using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return DeleteMe.Users;
        }

        [HttpGet("{index}")]
        public string Get(int index)
        {
            
            return DeleteMe.Users[index];
        }

        [HttpDelete("{index}")]
        public void Delete(int index)
        {
            DeleteMe.Users.RemoveAt(index);
        }

        [HttpPost]
        public void Post([FromBody] string user)
        {
            DeleteMe.Users.Add(user);
        }

        [HttpPut("{index}")]
        public void Put(int index, [FromBody] string user)
        {
            DeleteMe.Users[index] = user;
        }
    }
}