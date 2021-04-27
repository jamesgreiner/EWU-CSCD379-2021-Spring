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
                Assert.AreEqual("userManager", e.ParamName);
                return;
            }
           
            Assert.Fail("No exception was thrown.");
        }
        
        
        [TestMethod]
        public void Get_WithData_ReturnsUsers()
        {
            //Arrange
            UsersController controller = new(new UserManager());

            //Act
            IEnumerable<User> users = controller.Get();

            //Assert
            Assert.IsTrue(users.Any());
            
        }

        [TestMethod]
        [DataRow(42)]
        [DataRow(98)]
        public void Get_WithId_ReturnsUserManager(int id)
        {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();
            manager.getUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(id);

            //Assert
            Assert.AreEqual(id, manager.GetUserId);
            Assert.Equals(expectedUser, result.Value);
        }

        [TestMethod]
        public void Get_WithNegativeId_ReturnsNotFound()
        {
            //Arrange
            TestableUserManager manager = new();
            UsersController controller = new(manager);
            User expectedUser = new();
            manager.getUser = expectedUser;

            //Act
            ActionResult<User?> result = controller.Get(-1);

            //Assert
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        private class TestableUserManager : IUserManager
        {
            public User Create(User user)
            {
            DeleteMe.Users.Add(user);
            return user;
            }

            public User getUser { get; set; }
            public int GetUserId { get; set; } 
            public User? GetItem(int id)
            {
                GetUserId = id;
                return getUser;
            }

            public ICollection<User> List()
            {
                return DeleteMe.Users;
            }

            public bool Remove(int id)
            {
                throw new System.NotImplementedException();
            }

            public User Save(User user)
            {
                throw new System.NotImplementedException();
            }
            }
    }
}