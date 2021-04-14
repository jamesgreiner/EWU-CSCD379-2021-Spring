using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View(MockData.Groups);
        }

         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Groups.Add(ViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        public IActionResult Edit(int id)
        {
            MockData.Groups[id].ID = id;
            return View(MockData.Groups[id]);
        }

        [HttpPost]
        public IActionResult Edit(GroupViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Groups[ViewModel.ID] = ViewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Groups.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}