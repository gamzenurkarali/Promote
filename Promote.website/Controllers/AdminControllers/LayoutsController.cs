﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promote.website.Models;

namespace Promote.website.Controllers.AdminControllers
{
    public class LayoutsController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LayoutsController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Layouts
        public async Task<IActionResult> Index()
        {
              return _context.layouts != null ? 
                          View(await _context.layouts.ToListAsync()) :
                          Problem("Entity set 'Context.layouts'  is null.");
        }

        // GET: Layouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.layouts == null)
            {
                return NotFound();
            }

            var layout = await _context.layouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (layout == null)
            {
                return NotFound();
            }

            return View(layout);
        }

        // GET: Layouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Layouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile LogoPath, IFormFile SocialMedia1Icon, IFormFile SocialMedia2Icon, IFormFile SocialMedia3Icon, IFormFile SocialMedia4Icon, [Bind("Id,LogoPath,FooterColor,HighlightColor,SocialMedia1Link,SocialMedia1Icon,SocialMedia2Link,SocialMedia2Icon,SocialMedia3Link,SocialMedia3Icon,SocialMedia4Link,SocialMedia4Icon")] Layout layout)
        {
            try
            {
                if (LogoPath != null && SocialMedia1Icon != null && SocialMedia2Icon != null && SocialMedia3Icon != null && SocialMedia4Icon != null)
                {
                    // Save files
                    layout.LogoPath = await SaveFile(LogoPath);
                    layout.SocialMedia1Icon = await SaveFile(SocialMedia1Icon);
                    layout.SocialMedia2Icon = await SaveFile(SocialMedia2Icon);
                    layout.SocialMedia3Icon = await SaveFile(SocialMedia3Icon);
                    layout.SocialMedia4Icon = await SaveFile(SocialMedia4Icon);

                    _context.Add(layout);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Please select all required files.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(layout);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "LayoutMedia", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        // GET: Layouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.layouts == null)
            {
                return NotFound();
            }

            var layout = await _context.layouts.FindAsync(id);
            if (layout == null)
            {
                return NotFound();
            }
            return View(layout);
        }

        // POST: Layouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile LogoPath, IFormFile SocialMedia1Icon, IFormFile SocialMedia2Icon, IFormFile SocialMedia3Icon, IFormFile SocialMedia4Icon, [Bind("Id,LogoPath,FooterColor,HighlightColor,SocialMedia1Link,SocialMedia1Icon,SocialMedia2Link,SocialMedia2Icon,SocialMedia3Link,SocialMedia3Icon,SocialMedia4Link,SocialMedia4Icon")] Layout layout)
        {
            if (id != layout.Id)
            {
                return NotFound();
            }

            try
            {
                var existingLayout = await _context.layouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                // Delete existing files
                if (LogoPath != null)
                {
                    await DeleteFileIfExists(existingLayout.LogoPath);
                }

                if (SocialMedia1Icon != null)
                {
                    await DeleteFileIfExists(existingLayout.SocialMedia1Icon);
                }

                if (SocialMedia2Icon != null)
                {
                    await DeleteFileIfExists(existingLayout.SocialMedia2Icon);
                }

                if (SocialMedia3Icon != null)
                {
                    await DeleteFileIfExists(existingLayout.SocialMedia3Icon);
                }

                if (SocialMedia4Icon != null)
                {
                    await DeleteFileIfExists(existingLayout.SocialMedia4Icon);
                }

                // Save new files or keep existing file names
                layout.LogoPath = LogoPath != null ? await SaveFile(LogoPath) : existingLayout.LogoPath;
                layout.SocialMedia1Icon = SocialMedia1Icon != null ? await SaveFile(SocialMedia1Icon) : existingLayout.SocialMedia1Icon;
                layout.SocialMedia2Icon = SocialMedia2Icon != null ? await SaveFile(SocialMedia2Icon) : existingLayout.SocialMedia2Icon;
                layout.SocialMedia3Icon = SocialMedia3Icon != null ? await SaveFile(SocialMedia3Icon) : existingLayout.SocialMedia3Icon;
                layout.SocialMedia4Icon = SocialMedia4Icon != null ? await SaveFile(SocialMedia4Icon) : existingLayout.SocialMedia4Icon;

                _context.Update(layout);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LayoutExists(layout.Id))
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
        private async Task DeleteFileIfExists(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "LayoutMedia", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        // GET: Layouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.layouts == null)
            {
                return NotFound();
            }

            var layout = await _context.layouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (layout == null)
            {
                return NotFound();
            }

            return View(layout);
        }

        // POST: Layouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.layouts == null)
            {
                return Problem("Entity set 'Context.layouts'  is null.");
            }
            var layout = await _context.layouts.FindAsync(id);
            if (layout != null)
            {
                _context.layouts.Remove(layout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LayoutExists(int id)
        {
          return (_context.layouts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
