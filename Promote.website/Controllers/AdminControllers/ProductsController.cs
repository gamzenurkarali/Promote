using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductsController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        //[Authorize]
        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.products != null ? 
                          View(await _context.products.ToListAsync()) :
                          Problem("Entity set 'Context.products'  is null.");
        }
        //[Authorize]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //[Authorize]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)]       //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Create(IFormFile ProductImageFileName, IFormFile DetailedDescriptionBgImage, [Bind("Id,ProductName,Fee,Description,Tab1Description,Tab2Description,Tab3Description,DetailedDescription")] Product product)
        {
            try
            {
                if (ProductImageFileName != null)
                {
                    product.ProductImageFileName = await SaveFile(ProductImageFileName);
                }

                if (DetailedDescriptionBgImage != null)
                {
                    product.DetailedDescriptionBgImage = await SaveFile(DetailedDescriptionBgImage);
                }

                
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(product);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        //[Authorize]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)]       //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Edit(int id, IFormFile ProductImageFileName, IFormFile DetailedDescriptionBgImage,[Bind("Id,ProductImageFileName,ProductName,Fee,Description,Tab1Description,Tab2Description,Tab3Description,DetailedDescriptionBgImage,DetailedDescription")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            try
            {
                var existingProduct = await _context.products.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                // Delete the existing files if new ones are provided
                if (ProductImageFileName != null)
                {
                    await DeleteFileIfExists(existingProduct.ProductImageFileName);
                    product.ProductImageFileName = await SaveFile(ProductImageFileName);
                }
                else
                {
                    product.ProductImageFileName = existingProduct.ProductImageFileName;
                }

                if (DetailedDescriptionBgImage != null)
                {
                    await DeleteFileIfExists(existingProduct.DetailedDescriptionBgImage);
                    product.DetailedDescriptionBgImage = await SaveFile(DetailedDescriptionBgImage);
                }
                else
                {
                    product.DetailedDescriptionBgImage = existingProduct.DetailedDescriptionBgImage;
                }


                _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(product);
        }
        private async Task DeleteFileIfExists(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        //[Authorize]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.products == null)
            {
                return Problem("Entity set 'Context.Products' is null.");
            }

            var product = await _context.products.FindAsync(id);

            if (product != null)
            {
                // Delete the associated files if they exist
                await DeleteFileIfExists(product.ProductImageFileName);
                await DeleteFileIfExists(product.DetailedDescriptionBgImage);

                _context.products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(int id)
        {
          return (_context.products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
