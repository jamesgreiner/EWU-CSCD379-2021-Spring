using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}