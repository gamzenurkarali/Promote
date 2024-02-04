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
    public class ProductDetailPagesController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductDetailPagesController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Router()
        {
            bool hasRecord = _context.productDetailPages.Any();

            int firstRecordId = hasRecord ? _context.productDetailPages.First().Id : 0;

            if (hasRecord)
            {
                return RedirectToAction("Edit", new { id = firstRecordId });
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        //[Authorize]
        // GET: ProductDetailPages/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                bool hasRecord = _context.productDetailPages.Any();

                int firstRecordId = hasRecord ? _context.productDetailPages.First().Id : 0;

                if (hasRecord)
                {
                    return RedirectToAction("Edit", new { id = firstRecordId });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }

        // POST: ProductDetailPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageHeader,Tab1Title,Tab2Title,Tab3Title,DetailedDescriptionTitle")] ProductDetailPage productDetailPage, IFormFile ImageHeader)
        {
            try
            {
                if (ImageHeader != null &&
                    !string.IsNullOrEmpty(productDetailPage.Tab1Title) &&
                    !string.IsNullOrEmpty(productDetailPage.Tab2Title) &&
                    !string.IsNullOrEmpty(productDetailPage.Tab3Title) &&
                    !string.IsNullOrEmpty(productDetailPage.DetailedDescriptionTitle))
                {
                    productDetailPage.ImageHeader = await SaveFile(ImageHeader);

                     
                    _context.Add(productDetailPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "ProductDetailPage successfully created!";
                    TempData["AlertClass"] = "alert-success";

                    return RedirectToAction("Router");
                    
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

            return View(productDetailPage);
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
        // GET: ProductDetailPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.productDetailPages == null)
                {
                    return NotFound();
                }

                var productDetailPage = await _context.productDetailPages.FindAsync(id);
                if (productDetailPage == null)
                {
                    return NotFound();
                }
                return View(productDetailPage);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
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

        // POST: ProductDetailPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageHeader,Tab1Title,Tab2Title,Tab3Title,DetailedDescriptionTitle")] ProductDetailPage productDetailPage, IFormFile ImageHeader)
        {
            if (id != productDetailPage.Id)
            {
                return NotFound();
            }

            try
            {
                var existingProductDetailPage = await _context.productDetailPages.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (productDetailPage.Tab1Title != null
                    && productDetailPage.Tab2Title != null
                    && productDetailPage.Tab3Title != null
                    && productDetailPage.DetailedDescriptionTitle != null)
                { 
                    if (ImageHeader != null)
                    {
                        await DeleteFileIfExists(existingProductDetailPage.ImageHeader);
                    }
                    else
                    {
                        productDetailPage.ImageHeader = existingProductDetailPage.ImageHeader;
                    }
                    productDetailPage.ImageHeader = ImageHeader != null ? await SaveFile(ImageHeader) : existingProductDetailPage.ImageHeader;

                    if (ModelState.IsValid)
                    {
                        _context.Update(productDetailPage);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Message"] = "ProductDetailPage updated successfully!";
                    TempData["AlertClass"] = "alert-success";

                    return RedirectToAction("Router");

                }
                else
                {
                    TempData["Message"] = "Please fill in all required fields.";
                    TempData["AlertClass"] = "alert-danger";
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailPageExists(productDetailPage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Router");
        }

        //[Authorize]
        // GET: ProductDetailPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.productDetailPages == null)
                {
                    return NotFound();
                }

                var productDetailPage = await _context.productDetailPages
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (productDetailPage == null)
                {
                    return NotFound();
                }

                return View(productDetailPage);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }

        // POST: ProductDetailPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.productDetailPages == null)
            {
                return Problem("Entity set 'Context.ProductDetailPages' is null.");
            }

            var productDetailPage = await _context.productDetailPages.FindAsync(id);

            if (productDetailPage != null)
            {
                // Delete the associated file if it exists
                await DeleteFileIfExists(productDetailPage.ImageHeader);

                _context.productDetailPages.Remove(productDetailPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProductDetailPageExists(int id)
        {
          return (_context.productDetailPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
