using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void Create_WithData_ReturnsUser(int id)
        {
            //Arrange
            UserRepository repo = new();

            //Act
            User user = new();
            user.Id = id;

            //Assert
            Assert.AreEqual(repo.Create(id), user);
        }



        [TestMethod]
        public void GetItem_WithId_ReturnsUser(int id)
        {
            //Arrange
            UserRepository repo = new();

            //Act
            User user = new();
            user.Id = id;

            //Assert
            Assert.AreEqual(repo.GetItem(id), user);
        }
    }
}
