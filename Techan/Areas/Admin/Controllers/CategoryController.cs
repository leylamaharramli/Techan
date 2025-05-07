using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Techan.ViewModels.Category;


namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Category> datas = [];
            datas = await _context.Categories.ToListAsync();
            List<CategoryGetVM> categories = [];
            foreach (var item in datas)
            {
                categories.Add(new CategoryGetVM()
                {
                    Name = item.Name,
                    Id = item.Id
                });
            }
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            Category category = new();
            category.Name = model.Name;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1) return BadRequest();
            int result = await _context.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue && id < 1) return BadRequest();
            var entity = await _context.Categories.Select(x => new CategoryUpdateVM
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (id.HasValue && id < 1)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vm);

            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();
            entity.Name = vm.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}