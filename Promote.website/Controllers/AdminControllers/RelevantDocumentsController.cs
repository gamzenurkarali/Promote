using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promote.website.Models;

namespace Promote.website.Controllers.AdminControllers
{
    public class RelevantDocumentsController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public RelevantDocumentsController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: RelevantDocuments
        public async Task<IActionResult> Index()
        {
              return _context.relevantDocuments != null ? 
                          View(await _context.relevantDocuments.ToListAsync()) :
                          Problem("Entity set 'Context.relevantDocuments'  is null.");
        }

        // GET: RelevantDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.relevantDocuments == null)
            {
                return NotFound();
            }

            var relevantDocument = await _context.relevantDocuments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (relevantDocument == null)
            {
                return NotFound();
            }

            return View(relevantDocument);
        }

        // GET: RelevantDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelevantDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Content, [Bind("ID,Title,Content,CreationDate,UpdateDate")] RelevantDocument relevantDocument)
        {
            try
            {

                 

                if (Content != null &&
                        relevantDocument.Title!=null)
                    { 
                        relevantDocument.Content = await SaveFile(Content);
                    relevantDocument.UpdateDate = DateTime.Now;
                    relevantDocument.CreationDate = DateTime.Now;
                    _context.Add(relevantDocument);
                        await _context.SaveChangesAsync();

                        TempData["Message"] = "Document successfully created!";
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

            return View(relevantDocument);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "RelevantDocuments", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        // GET: RelevantDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.relevantDocuments == null)
            {
                return NotFound();
            }

            var relevantDocument = await _context.relevantDocuments.FindAsync(id);
            if (relevantDocument == null)
            {
                return NotFound();
            }
            return View(relevantDocument);
        }
        // POST: RelevantDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile Content, [Bind("ID,Title,Content,CreationDate,UpdateDate")] RelevantDocument relevantDocument)
        {
           

            if (id != relevantDocument.ID)
            {
                return NotFound();
            }

            try
            {

                if (relevantDocument.Title != null)
                {
                    var existingDocument = await _context.relevantDocuments.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

                    if (Content != null)
                    {
                        await DeleteFileIfExists(existingDocument.Content);
                    }
                    else
                    {
                        relevantDocument.Content = existingDocument.Content;
                    }

                    relevantDocument.Content = Content != null ? await SaveFile(Content) : existingDocument.Content;
                    relevantDocument.UpdateDate = DateTime.Now;
                    relevantDocument.CreationDate = existingDocument.CreationDate;
                    _context.Update(relevantDocument);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Document successfully updated!";
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
                if (!RelevantDocumentExists(relevantDocument.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Edit",relevantDocument.ID);
        }
        private async Task DeleteFileIfExists(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "RelevantDocuments", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        // GET: RelevantDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.relevantDocuments == null)
            {
                return NotFound();
            }

            var relevantDocument = await _context.relevantDocuments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (relevantDocument == null)
            {
                return NotFound();
            }

            return View(relevantDocument);
        }
        // POST: RelevantDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.relevantDocuments == null)
            {
                return Problem("Entity set 'Context.relevantDocuments' is null.");
            }

            var relevantDocument = await _context.relevantDocuments.FindAsync(id);

            if (relevantDocument != null)
            {
                // Dosyaların silme işlemleri
                await DeleteFileIfExists(relevantDocument.Content);

                _context.relevantDocuments.Remove(relevantDocument);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RelevantDocumentExists(int id)
        {
            return (_context.relevantDocuments?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
