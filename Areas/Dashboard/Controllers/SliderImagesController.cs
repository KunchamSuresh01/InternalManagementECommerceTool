using InternalManagementECommerceTool.Data;
using InternalManagementECommerceTool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InternalManagementECommerceTool.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SliderImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SliderImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SliderImages.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImages = await _context.SliderImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderImages == null)
            {
                return NotFound();
            }

            return View(sliderImages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderImages sliderImages, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError(nameof(Product.Image), "Image is required.");
                    return View(sliderImages);
                }

                var imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages"));
                }

                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages", imageName);

                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                sliderImages.Image = $"/img/SliderImages/{imageName}";
                _context.Add(sliderImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sliderImages);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImages = await _context.SliderImages.FindAsync(id);
            if (sliderImages == null)
            {
                return NotFound();
            }
            return View(sliderImages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,SortOrder")] SliderImages sliderImages)
        {
            if (id != sliderImages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sliderImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderImagesExists(sliderImages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sliderImages);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImages = await _context.SliderImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderImages == null)
            {
                return NotFound();
            }

            return View(sliderImages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sliderImages = await _context.SliderImages.FindAsync(id);
            if (sliderImages != null)
            {
                _context.SliderImages.Remove(sliderImages);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderImagesExists(int id)
        {
            return _context.SliderImages.Any(e => e.Id == id);
        }
    }
}
