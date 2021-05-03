//must have functional tests for all endpoints in the UserController that access the API
//must use a test double for the API service - use it to validate teh controllers interaction with the API - CANNOT CALL API


using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        private WebApplicationFactory Factory { get; } = new();
        
        [TestMethod]
        public async Task Index_WithUsers_InvokesGetAllAsync()
        {
            //Arrange
            User user1 = new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            };
            User user2 = new()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith"
            };

            TestableUsersClient usersClient = Factory.Client;
            usersClient.GetAllUsersReturnValue = new List<User>()
            {
                user1, user2
            };

            HttpClient client = Factory.CreateClient();

            //Act
            HttpResponseMessage response = await client.GetAsync("/Users/");

            string foo = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
        }



        [TestMethod]
        public async Task Create_WithValidModel_InvokesPostAsync()
        {
            //Arrange
            HttpClient client = Factory.CreateClient();
            TestableUsersClient usersClient = Factory.Client;
            Dictionary<string, string?> values = new()
            {
                {nameof(UserViewModel.FirstName), "John"},
                {nameof(UserViewModel.LastName), "Doe"}
            };
            FormUrlEncodedContent content = new(values!);

            //Act
            HttpResponseMessage response = await client.PostAsync("/Users/Create/", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(1, usersClient.PostAsyncInvocationCount);
            Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
            Assert.AreEqual("John", usersClient.PostAsyncInvokedParameters[0].FirstName);
            //Assert.AreEqual(newUser.LastName, usersClient.PostAsyncInvokedParameters[0].LastName);
        }
    }
}