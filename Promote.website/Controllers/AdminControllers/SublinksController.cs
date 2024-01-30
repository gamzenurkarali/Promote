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
    public class SublinksController : Controller
    {
        private readonly Context _context;

        public SublinksController(Context context)
        {
            _context = context;
        }
        //[Authorize]
        // GET: Sublinks
        public async Task<IActionResult> Index()
        {
              return _context.sublinks != null ? 
                          View(await _context.sublinks.ToListAsync()) :
                          Problem("Entity set 'Context.sublinks'  is null.");
        }
        //[Authorize]
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
        //[Authorize]
        // GET: Sublinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sublinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogoPath,PageName")] Sublink sublink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sublink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sublink);
        }
        //[Authorize]
        // GET: Sublinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Sublinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogoPath,PageName")] Sublink sublink)
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
        //[Authorize]
        // GET: Sublinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
