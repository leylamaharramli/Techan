using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Slider> datas = [];
            using (var context = new TechanDbContext())
            {
                datas = await context.Sliders.ToListAsync();
            }
            List<SliderGetVM> sliders = [];
            foreach (var item in datas)
            {
                sliders.Add(new SliderGetVM()
                {
                    BigTitle=item.BigTitle,
                    Id=item.Id,
                    ImageUrl=item.ImageUrl,
                    Link=item.Link,
                    Offer=item.Offer,
                    Title=item.Title
                });
            }
            return View(sliders);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
