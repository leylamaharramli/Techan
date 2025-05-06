using Microsoft.AspNetCore.Mvc;

namespace Techan.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
