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

        public ProductDetailPagesController(Context context)
        {
            _context = context;
        }
        //[Authorize]
        // GET: ProductDetailPages
        public async Task<IActionResult> Index()
        {
              return _context.productDetailPages != null ? 
                          View(await _context.productDetailPages.ToListAsync()) :
                          Problem("Entity set 'Context.productDetailPages'  is null.");
        }
        //[Authorize]
        // GET: ProductDetailPages/Details/5
        public async Task<IActionResult> Details(int? id)
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
        //[Authorize]
        // GET: ProductDetailPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductDetailPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageHeader,Tab1Title,Tab2Title,Tab3Title,DetailedDescriptionTitle")] ProductDetailPage productDetailPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productDetailPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productDetailPage);
        }
        //[Authorize]
        // GET: ProductDetailPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: ProductDetailPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageHeader,Tab1Title,Tab2Title,Tab3Title,DetailedDescriptionTitle")] ProductDetailPage productDetailPage)
        {
            if (id != productDetailPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDetailPage);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(productDetailPage);
        }
        //[Authorize]
        // GET: ProductDetailPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ProductDetailPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.productDetailPages == null)
            {
                return Problem("Entity set 'Context.productDetailPages'  is null.");
            }
            var productDetailPage = await _context.productDetailPages.FindAsync(id);
            if (productDetailPage != null)
            {
                _context.productDetailPages.Remove(productDetailPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDetailPageExists(int id)
        {
          return (_context.productDetailPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
