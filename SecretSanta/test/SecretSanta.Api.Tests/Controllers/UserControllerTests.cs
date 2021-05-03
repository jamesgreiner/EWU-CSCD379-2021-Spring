//must use integration tests
//all endpoints must be tested
//must use test double for IUserRepository


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using SecretSanta.Api.Dto;
using Microsoft.AspNetCore.Http;
using SecretSanta.Api.Tests.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public async Task Put_WithValidData_UpdatesEvent()
        {
            //Arrange
            WebApplicationFactory factory = new();
            TestableUserRepository repository = factory.Repository;
            User foundUser = new User 
            {
                Id = 42
            };

            //repository.GetItemUser = foundUser;
            HttpClient client = factory.CreateClient();
            UpdateUser updateUser = new()
            {
                FirstName = "Kobe",
                LastName = "Bryant"
            };

            //Act
            HttpResponseMessage response = await client.PutAsJsonAsync("api/users/42", updateUser);


            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Kobe", repository.SavedUser?.FirstName);
        }
    }
}
