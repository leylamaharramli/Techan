using Microsoft.AspNetCore.Mvc;

namespace TechanProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
