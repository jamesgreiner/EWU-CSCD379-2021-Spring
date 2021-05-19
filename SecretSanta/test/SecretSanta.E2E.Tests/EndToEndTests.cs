using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using System.Linq;

namespace SecretSanta.E2E.Tests
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<Web.Startup, UserGroup.Api.Startup> Server;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Server = new();
        }

        // This test checks that 'Secret Santa' is diplayed in the navigation banner when on the homepage
        [TestMethod]
        public async Task LaunchHomepage()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            var headerContent = await page.GetTextContentAsync("body > header > div > a");
            Assert.AreEqual("Secret Santa", headerContent);
        }



        // Test for navigating to the Users page
        [TestMethod]
        public async Task NavigateToUsers()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Users");
            await page.ScreenshotAsync("users.png");
        }



        // Test for navigating to the Groups page
        [TestMethod]
        public async Task NavigateToGroups()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Groups");
            await page.ScreenshotAsync("groups.png");
        }



        // Test for navigating to the Gifts page
        [TestMethod]
        public async Task NavigateToGifts()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.ScreenshotAsync("gifts.png");
        }



        // Test that a gift can be added and the total number of gifts increases by one
        [TestMethod]
        public async Task CreateGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            var gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int currentNumberOfGifts = gifts.Count();

            // open the create page
            await page.ClickAsync("text=Create");
            
            await page.TypeAsync("input#Title", "Lego Millenium Falcon");
            await page.TypeAsync("input#Description", "This is a lego replica of the Millenium Falcon from Star Wars.");
            await page.TypeAsync("input#Url", "https://www.lego.com/en-us/product/millennium-falcon-75192");
            await page.TypeAsync("input#Priority", "1");
            await page.SelectOptionAsync("select#UserId", "2");
        
            // create the gift
            await page.ClickAsync("text=Create");

            // ensure the number of gifts increased by one
            gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int newNumberOfGifts = gifts.Count();
            Assert.AreEqual(newNumberOfGifts, currentNumberOfGifts + 1);
        }



        // This will ensure the last gift in the list can be updated
        [TestMethod]
        public async Task UpdateLastGift() 
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions 
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            // go to Gifts page
            await page.ClickAsync("text=Gifts");

            // grab last gift
            await page.ClickAsync("body > section > section > section:last-child");

            // select all text
            await page.ClickAsync("input#Title", clickCount:3);

            // fill in new title of gift
            await page.TypeAsync("input#Title", "Updated Gift");

            // update gift
            await page.ClickAsync("text=Update");

            await page.WaitForSelectorAsync("body > section > section > section:last-child");
            var giftContent = await page.GetTextContentAsync("body > section > section > section:last-child");
            
            Assert.IsTrue(giftContent.Contains("Updated Gift"));
        }



        [TestMethod]
        public async Task DeleteLastGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");
            
            var gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int currentNumberOfGifts = gifts.Count();

            page.Dialog += (_, args) => args.Dialog.AcceptAsync();

            await page.ClickAsync("body > section > section > section:last-child > a > section > form > button");
            gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int newNumberOfGifts = gifts.Count();
            Assert.AreEqual(newNumberOfGifts, currentNumberOfGifts - 1);
        }
    }
}