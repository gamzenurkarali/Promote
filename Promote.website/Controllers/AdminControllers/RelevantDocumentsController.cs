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
    public class RelevantDocumentsController : Controller
    {
        private readonly Context _context;

        public RelevantDocumentsController(Context context)
        {
            _context = context;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Content,CreationDate,UpdateDate")] RelevantDocument relevantDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relevantDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relevantDocument);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content,CreationDate,UpdateDate")] RelevantDocument relevantDocument)
        {
            if (id != relevantDocument.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relevantDocument);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(relevantDocument);
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
                return Problem("Entity set 'Context.relevantDocuments'  is null.");
            }
            var relevantDocument = await _context.relevantDocuments.FindAsync(id);
            if (relevantDocument != null)
            {
                _context.relevantDocuments.Remove(relevantDocument);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelevantDocumentExists(int id)
        {
          return (_context.relevantDocuments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
