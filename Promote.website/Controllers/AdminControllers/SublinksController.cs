using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promote.website.Models;

namespace Promote.website.Controllers.AdminControllers
{
    public class SublinksController : Controller
    {
        private readonly Context _context;

        public SublinksController(Context context)
        {
            _context = context;
        }

        // GET: Sublinks
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.sublinks != null ?
                          View(await _context.sublinks.ToListAsync()) :
                          Problem("Entity set 'Context.sublinks'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

            // GET: Sublinks/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.sublinks == null)
            {
                return NotFound();
            }

            var sublink = await _context.sublinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sublink == null)
            {
                return NotFound();
            }

            return View(sublink);
        }

        // GET: Sublinks/Create
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

        // POST: Sublinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PagePath,PageName")] Sublink sublink)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sublink);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Sublink successfully created!";
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

            return View(sublink);
        }


        // GET: Sublinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.sublinks == null)
                {
                    return NotFound();
                }

                var sublink = await _context.sublinks.FindAsync(id);
                if (sublink == null)
                {
                    return NotFound();
                }
                return View(sublink);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }

        // POST: Sublinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PagePath,PageName")] Sublink sublink)
        {
            if (id != sublink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sublink);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Sublink successfully updated!";
                    TempData["AlertClass"] = "alert-success";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SublinkExists(sublink.Id))
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
            return View(sublink);
        }

        // GET: Sublinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.sublinks == null)
                {
                    return NotFound();
                }

                var sublink = await _context.sublinks
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (sublink == null)
                {
                    return NotFound();
                }

                return View(sublink);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            }

        // POST: Sublinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.sublinks == null)
            {
                return Problem("Entity set 'Context.sublinks'  is null.");
            }
            var sublink = await _context.sublinks.FindAsync(id);
            if (sublink != null)
            {
                _context.sublinks.Remove(sublink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SublinkExists(int id)
        {
          return (_context.sublinks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
