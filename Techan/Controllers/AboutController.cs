using Microsoft.AspNetCore.Mvc;

namespace TechanProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
