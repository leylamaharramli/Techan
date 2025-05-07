using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Brand;


namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Brand> datas = [];
            datas = await _context.Brands.ToListAsync();
            List<BrandGetVM> brands = [];
            foreach (var item in datas)
            {
                brands.Add(new BrandGetVM()
                {
                    Id = item.Id,
                    ImagePath = item.ImagePath,
                    Name = item.Name
                });
            }
            return View(brands);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM model)
        {
            if(model.ImagePath != null)
            {
                if (!model.ImagePath.ContentType.StartsWith("image"))
                {
                    string ext = Path.GetExtension(model.ImagePath.FileName);
                    ModelState.AddModelError("ImagePath", "File must be page format");
                }
                if (model.ImagePath.Length / 1024 > 200)
                {
                    ModelState.AddModelError("ImagePath", "Size must not be greater than 200 kb");
                }
            }
            if (!ModelState.IsValid)
                return View(model);
            string NewName = Path.GetRandomFileName() + Path.GetExtension(model.ImagePath!.FileName);
            string path = Path.Combine("wwwroot", "imgs", "brands", NewName);
            await using (FileStream fs = System.IO.File.Create(path))
            {
                await model.ImagePath.CopyToAsync(fs);
            }
            Brand brand = new()
            {
                ImagePath = NewName,
                Name = model.Name
            };
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1) return BadRequest();
            int result = await _context.Brands.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue && id < 1) return BadRequest();
            var entity = await _context.Brands.Select(x => new BrandUpdateVM
            {
                Id = x.Id,
                ImagePath = x.ImagePath,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, BrandUpdateVM vm)
        {
            if (id.HasValue && id < 1)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vm);

            var entity = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();

            if (vm.Image != null)
            {
                if (!vm.Image.ContentType.StartsWith("image"))
                {
                    string ext = Path.GetExtension(vm.Image.FileName);
                    ModelState.AddModelError("ImagePath", "File must be page format");
                }
                if (vm.Image.Length / 1024 > 200)
                {
                    ModelState.AddModelError("ImagePath", "Size must not be greater than 200 kb");
                }
                string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.Image.FileName);
                string newPath = Path.Combine("wwwroot", "imgs", "brands", newFileName);
                await using (FileStream fs = System.IO.File.Create(newPath))
                {
                    await vm.Image.CopyToAsync(fs);
                }
                entity.ImagePath = newFileName;
                entity.Name = vm.Name;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
