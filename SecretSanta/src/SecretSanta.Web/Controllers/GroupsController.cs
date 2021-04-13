using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        static List<GroupViewModel> Groups = new List<GroupViewModel>
        {
            new GroupViewModel{GroupName="IntelliTect"},
            new GroupViewModel{GroupName="Microsoft"}
        };
        public IActionResult Index()
        {
            return View(Groups);
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
                Groups.Add(ViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        public IActionResult Edit(int id)
        {
            Groups[id].ID = id;
            return View(Groups[id]);
        }

        [HttpPost]
        public IActionResult Edit(GroupViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                Groups[ViewModel.ID] = ViewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Groups.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}