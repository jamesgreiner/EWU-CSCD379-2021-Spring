using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.Dto;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private property
        private IUserRepository UserRepository { get; }

        //constructor
        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
        
            User? returnedUser = UserRepository.GetItem(id);
            return returnedUser;
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            if (UserRepository.Remove(id))
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

            return UserRepository.Create(user);
        }



        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateUser? updatedUser)
        {
            if (updatedUser is null)
            {
                return BadRequest();
            }
            
            User? foundUser = UserRepository.GetItem(id);
            if (foundUser is not null)
            {
                if (!string.IsNullOrWhiteSpace(updatedUser.FirstName) && !string.IsNullOrWhiteSpace(updatedUser.LastName))
                {
                    foundUser.FirstName = updatedUser.FirstName;
                    foundUser.LastName = updatedUser.LastName;
                }
                
                UserRepository.Save(foundUser);
                return Ok();
            }

            return NotFound();
        }
    }
}