using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View(MockData.Users);
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
                MockData.Users.Add(ViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        public IActionResult Edit(int id)
        {
            MockData.Users[id].ID = id;
            return View(MockData.Users[id]);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Users[ViewModel.ID] = ViewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Users.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}