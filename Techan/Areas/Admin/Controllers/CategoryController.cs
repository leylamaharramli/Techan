using Microsoft.AspNetCore.Mvc;
using Techan.DataAccessLayer;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController (TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Slider> datas = [];
            datas = await _context.Sliders.ToListAsync();
            List<SliderGetVM> sliders = [];
            foreach (var item in datas)
            {
                sliders.Add(new SliderGetVM()
                {
                    BigTitle = item.BigTitle,
                    Id = item.Id,
                    ImagePath = item.ImagePath,
                    Link = item.Link,
                    LittleTitle = item.LittleTitle,
                    Offer = item.Offer,
                    Title = item.Title
                });
            }
            return View(sliders);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            string NewName = Path.GetRandomFileName() + Path.GetExtension(model.Image!.FileName);
            string path = Path.Combine("imgs", "sliders", NewName);
            await using (FileStream fs = System.IO.File.Create(NewName))
            {
                await model.Image.CopyToAsync(fs);
            }
            Slider slider = new()
            {
                BigTitle = model.BigTitle,
                ImagePath = NewName,
                Link = model.Link,
                LittleTitle = model.LittleTitle,
                Offer = model.Offer,
                Title = model.Title
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1) return BadRequest();
            int result = await _context.Sliders.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
