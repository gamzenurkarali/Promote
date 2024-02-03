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
    public class ProductListPagesController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductListPagesController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Router()
        { 
            bool hasRecord = _context.productLists.Any();
             
            int firstRecordId = hasRecord ? _context.productLists.First().Id : 0;

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
        // GET: ProductListPages/Create
        public IActionResult Create()
        { 
            bool hasRecord = _context.productLists.Any();
             
            int firstRecordId = hasRecord ? _context.productLists.First().Id : 0;

            if (hasRecord)
            { 
                return RedirectToAction("Edit", new { id = firstRecordId });
            }
            else
            {
                return View();
            }
            
        }

        // POST: ProductListPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ProductListPage productListPage, IFormFile ImageHeader)
        {
            try
            {
                if (ImageHeader != null && ModelState.IsValid)
                {
                    productListPage.ImageHeader = await SaveFile(ImageHeader);

                    _context.Add(productListPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "ProductListPage successfully created!";
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

            return View(productListPage);
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
        // GET: ProductListPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.productLists == null)
            {
                return NotFound();
            }

            var productListPage = await _context.productLists.FindAsync(id);
            if (productListPage == null)
            {
                return NotFound();
            }
            return View(productListPage);
        }

        // POST: ProductListPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageHeader")] ProductListPage productListPage, IFormFile ImageHeader)
        {
            if (id != productListPage.Id)
            {
                return NotFound();
            }

            try
            {
                var existingProductListPage = await _context.productLists.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (ImageHeader != null && ModelState.IsValid)
                {
                    // Delete the existing file if a new one is provided
                    if (ImageHeader != null)
                    {
                        await DeleteFileIfExists(existingProductListPage.ImageHeader);
                    }
                    else
                    {
                        productListPage.ImageHeader = existingProductListPage.ImageHeader;
                    }
                    // Save the new file or keep the existing one
                    productListPage.ImageHeader = ImageHeader != null ? await SaveFile(ImageHeader) : existingProductListPage.ImageHeader;

                    if (ModelState.IsValid)
                    {
                        _context.Update(productListPage);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "ProductListPage successfully updated!";
                        TempData["AlertClass"] = "alert-success";

                        return RedirectToAction("Router");
                    }

                    
                }
                else
                {
                    TempData["Message"] = "Please fill in all required fields.";
                    TempData["AlertClass"] = "alert-danger";
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductListPageExists(productListPage.Id))
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
        // GET: ProductListPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.productLists == null)
            {
                return NotFound();
            }

            var productListPage = await _context.productLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productListPage == null)
            {
                return NotFound();
            }

            return View(productListPage);
        }

        // POST: ProductListPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.productLists == null)
            {
                return Problem("Entity set 'Context.ProductLists' is null.");
            }

            var productListPage = await _context.productLists.FindAsync(id);

            if (productListPage != null)
            {
                // Delete the associated file if it exists
                await DeleteFileIfExists(productListPage.ImageHeader);

                _context.productLists.Remove(productListPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProductListPageExists(int id)
        {
          return (_context.productLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
