using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void Constructor_WithNullUserManager_ThrowsAppropriateException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UsersController(null!));
            
            try
            {
                new UsersController(null!);
            }
            catch(ArgumentNullException e)
            {
                Assert.AreEqual("userRepository", e.ParamName);
                return;
            }
           
            Assert.Fail("No exception was thrown.");
        }
        
        

        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            UsersController controller = new(new UserRepository());

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsTrue(users.Any());
            
        }



        [TestMethod]
        [DataRow(42)]
        [DataRow(98)]
        public void Get_WithId_ReturnsUserRepoository(int id)
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();
            manager.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, manager.GetItemId);
            Assert.Equals(expectedUser, result.Value);
        }



        [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();
            manager.GetItemUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }


        [TestMethod]
        public void Delete_WithId_ReturnsOk(int id)
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);
            
            //Act
            ActionResult<User?> result = controller.Delete(id);

            //Assert
            Assert.IsTrue(result.Result is OkResult);
        }


        
        [TestMethod]
        public void Delete_WithBadId_ReturnsNotFound()
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);

            //Act
            ActionResult<User?> result = controller.Delete(999999999);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }


        [TestMethod]
        public void Delete_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);

            //Act
            ActionResult<User?> result = controller.Delete(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }



        [TestMethod]
        public void Post_WithData_ReturnsNewUser()
        {
            //Arrange
            TestableUserRepository manager = new();
            UsersController controller = new(manager);
            User user = new() {Id = 4, FirstName="John", LastName="Stockton"};

            //Act


            //Assert
        }



//------------------------------private class for testing-----------------------------------------------
        private class TestableUserRepository : IUserRepository
        {
            public User Create(User user)
            {
                DeleteMe.Users.Add(user);
                return user;
            }



            public User? GetItemUser { get; set; }

            public int GetItemId { get; set; }

            public User? GetItem(int id)
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                return DeleteMe.Users.FirstOrDefault(x => x.Id==id);
            }



            public ICollection<User> List()
            {
                return DeleteMe.Users;
            }



            public bool Remove(int id)
            {
                User? foundUser = DeleteMe.Users.FirstOrDefault(x => x.Id == id);
                if (foundUser is not null)
                {
                    DeleteMe.Users.Remove(foundUser);
                    return true;
                } 

                return false;
            }



            public void Save(User user)
            {
                Remove(user.Id);
                Create(user);
            }
        }
    }
}