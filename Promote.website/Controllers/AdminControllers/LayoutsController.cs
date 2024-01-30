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
    public class LayoutsController : Controller
    {
        private readonly Context _context;

        public LayoutsController(Context context)
        {
            _context = context;
        }

        // GET: Layouts
        public async Task<IActionResult> Index()
        {
              return _context.layouts != null ? 
                          View(await _context.layouts.ToListAsync()) :
                          Problem("Entity set 'Context.layouts'  is null.");
        }
        //[Authorize]
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
        //[Authorize]
        // GET: Layouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Layouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogoPath,FooterColor,HighlightColor")] Layout layout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(layout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(layout);
        }
        //[Authorize]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogoPath,FooterColor,HighlightColor")] Layout layout)
        {
            if (id != layout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(layout);
        }
        //[Authorize]
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
