using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserManager UserManager { get; }
        public UsersController(IUserManager userManager)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return DeleteMe.Users;
        }

        [HttpGet("{id}")]
        public User? Get(int id)
        {
            return UserManager.GetItem(id);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            DeleteMe.Users.RemoveAt(id);
            return Ok();
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            DeleteMe.Users.Add(user);
        }

        [HttpPut("{index}")]
        public void Put(int index, [FromBody] User user)
        {
            DeleteMe.Users[index] = user;
        }
    }
}