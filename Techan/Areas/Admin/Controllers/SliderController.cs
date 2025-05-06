using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Sliders;


namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(TechanDbContext _context) : Controller
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

        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue && id < 1) return BadRequest();
            var entity = await _context.Sliders.Select(x => new SliderUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                BigTitle = x.BigTitle,
                Offer = x.Offer,
                Link = x.Link,
                Image = x.Image,
                LittleTitle = x.LittleTitle,
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            if (id.HasValue && id < 1)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(vm);
            var entity = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();
            entity.BigTitle = vm.BigTitle;
            entity.Offer = vm.Offer;
            entity.Link = vm.Link;
            entity.LittleTitle = vm.LittleTitle;
            entity.Title = vm.Title;
            if (vm.ImagePath != null && vm.ImagePath.Length > 0)
            {
                string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.Image!.FileName);
                string newpath = Path.Combine("imgs", "sliders", newFileName);
                await using (FileStream fs = System.IO.File.Create(newFileName))
                {
                    await vm.Image.CopyToAsync(fs);
                }
                entity.ImagePath = newFileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}