using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        static List<UserViewModel> Users = new List<UserViewModel>
        {
            new UserViewModel{FirstName = "John ", LastName="Smith"},
            new UserViewModel{FirstName = "Jane ", LastName="Smith"}
        };
        public IActionResult Index()
        {
            return View(Users);
        }
    }
}