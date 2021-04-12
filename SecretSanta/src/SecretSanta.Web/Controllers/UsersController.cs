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

         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                Users.Add(ViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(ViewModel);
           
        }

        public IActionResult Edit(int id)
        {
            return View(Users[id]);
        }
    }
}