using Microsoft.AspNetCore.Mvc;

namespace TechanProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
