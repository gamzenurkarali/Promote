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

        public ProductListPagesController(Context context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,ImageHeader")] ProductListPage productListPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productListPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productListPage);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageHeader")] ProductListPage productListPage)
        {
            if (id != productListPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productListPage);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(productListPage);
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
                return Problem("Entity set 'Context.productLists'  is null.");
            }
            var productListPage = await _context.productLists.FindAsync(id);
            if (productListPage != null)
            {
                _context.productLists.Remove(productListPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductListPageExists(int id)
        {
          return (_context.productLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
