using Microsoft.AspNetCore.Mvc;

namespace TechanProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
