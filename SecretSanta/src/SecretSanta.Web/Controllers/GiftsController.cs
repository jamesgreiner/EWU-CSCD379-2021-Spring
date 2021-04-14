using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IActionResult Index()
        {
            return View(MockData.Gifts);
        }

         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GiftViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Gifts.Add(ViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        public IActionResult Edit(int id)
        {
            MockData.Gifts[id].ID = id;
            return View(MockData.Gifts[id]);
        }

        [HttpPost]
        public IActionResult Edit(GiftViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Gifts[ViewModel.ID] = ViewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Gifts.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}