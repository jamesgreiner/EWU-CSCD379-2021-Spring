using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.Dto;

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
        public ActionResult<User?> Get(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
        
            User? returnedUser = UserManager.GetItem(id);
            return returnedUser;
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            if (UserManager.Remove(id))
            {
                return Ok();
            }

            return NotFound();
        }



        [HttpPost]
        public ActionResult<User> Post([FromBody] User? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            return UserManager.Create(user);
        }



        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateUser? updatedUser)
        {
            if (updatedUser is null)
            {
                return BadRequest();
            }
            
            User? foundUser = UserManager.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = updatedUser.FirstName;
                foundUser.LastName = updatedUser.LastName;

                UserManager.Save(foundUser);
                return Ok();
            }

            return NotFound();
        }
    }
}