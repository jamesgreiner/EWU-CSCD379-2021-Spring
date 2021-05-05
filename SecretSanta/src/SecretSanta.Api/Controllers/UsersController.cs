using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.Dto;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }



        public UsersController(IUserRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<User> Get()
        {
            return Repository.List();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IdNameUser?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null)
            {
                return NotFound();
            } 
            
            IdNameUser dtoUser = new();
            dtoUser.Id = user.Id;
            dtoUser.FirstName = user.FirstName;
            dtoUser.LastName = user.LastName;
            
            return dtoUser;
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public ActionResult<User?> Post([FromBody] IdNameUser? dtoUser)
        {
            if (dtoUser is null)
            {
                return BadRequest();
            }

            User user = new();
        
            return Repository.Create(user);
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, [FromBody] User? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = user.FirstName ?? "";
                foundUser.LastName = user.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}