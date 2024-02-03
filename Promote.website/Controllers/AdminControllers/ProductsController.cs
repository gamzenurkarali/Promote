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
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.products != null ?
                          View(await _context.products.ToListAsync()) :
                          Problem("Entity set 'Context.products'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Create(IFormFile ProductImageFileName, IFormFile DetailedDescriptionBgImage, [Bind("Id,ProductName,Fee,Description,Tab1Description,Tab2Description,Tab3Description,DetailedDescription")] Product product)
        {
            try
            {
                

                if (ProductImageFileName != null && DetailedDescriptionBgImage != null &&
                    !string.IsNullOrEmpty(product.ProductName) &&
                    !string.IsNullOrEmpty(product.Description) &&
                    !string.IsNullOrEmpty(product.Tab1Description) &&
                    !string.IsNullOrEmpty(product.Tab2Description) &&
                    !string.IsNullOrEmpty(product.Tab3Description) &&
                    !string.IsNullOrEmpty(product.DetailedDescription))
                {
                    
                        product.ProductImageFileName = await SaveFile(ProductImageFileName);
                   

                     
                        product.DetailedDescriptionBgImage = await SaveFile(DetailedDescriptionBgImage);
                     
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Product successfully created!";
                    TempData["AlertClass"] = "alert-success";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Please fill in all required fields.";
                    TempData["AlertClass"] = "alert-danger";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An error occurred: {ex.Message}";
                TempData["AlertClass"] = "alert-danger";
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
            if (HttpContext.Session.GetString("UserId") != null)
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
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
                if (product.ProductName != null
                    && product.Description != null
                    && product.Tab1Description != null
                    && product.Tab2Description != null
                    && product.Tab3Description != null
                    && product.DetailedDescription != null)
                {
                     
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

                    TempData["Message"] = "Product successfully updated!";
                    TempData["AlertClass"] = "alert-success";
                    return RedirectToAction(nameof(Index));

                }
                else
                {

                    TempData["Message"] = "Please fill in all required fields.";
                    TempData["AlertClass"] = "alert-danger";
                }
               
               
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

            return RedirectToAction("Edit",id);
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
            if (HttpContext.Session.GetString("UserId") != null)
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
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
