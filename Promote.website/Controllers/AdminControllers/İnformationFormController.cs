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
    public class İnformationFormController : Controller
    {
        private readonly Context _context;

        public İnformationFormController(Context context)
        {
            _context = context;
        }

        // GET: İnformationForm
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.İnformationForm != null ?
                          View(await _context.İnformationForm.ToListAsync()) :
                          Problem("Entity set 'Context.İnformationForm'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

            // GET: İnformationForm/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.İnformationForm == null)
            {
                return NotFound();
            }

            var İnformationForm = await _context.İnformationForm
                .FirstOrDefaultAsync(m => m.İnformationFormId == id);
            if (İnformationForm == null)
            {
                return NotFound();
            }

            return View(İnformationForm);
        }

        // GET: İnformationForm/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

            // POST: İnformationForm/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("İnformationFormId,Email")] İnformationForm İnformationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(İnformationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(İnformationForm);
        }

        // GET: İnformationForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.İnformationForm == null)
                {
                    return NotFound();
                }

                var İnformationForm = await _context.İnformationForm.FindAsync(id);
                if (İnformationForm == null)
                {
                    return NotFound();
                }
                return View(İnformationForm);
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

            // POST: İnformationForm/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("İnformationFormId,Email")] İnformationForm İnformationForm)
        {
            if (id != İnformationForm.İnformationFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(İnformationForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!İnformationFormExists(İnformationForm.İnformationFormId))
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
            return View(İnformationForm);
        }

        // GET: İnformationForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.İnformationForm == null)
                {
                    return NotFound();
                }

                var İnformationForm = await _context.İnformationForm
                    .FirstOrDefaultAsync(m => m.İnformationFormId == id);
                if (İnformationForm == null)
                {
                    return NotFound();
                }

                return View(İnformationForm);
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

            // POST: İnformationForm/Delete/5
            [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.İnformationForm == null)
            {
                return Problem("Entity set 'Context.İnformationForm'  is null.");
            }
            var İnformationForm = await _context.İnformationForm.FindAsync(id);
            if (İnformationForm != null)
            {
                _context.İnformationForm.Remove(İnformationForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool İnformationFormExists(int id)
        {
          return (_context.İnformationForm?.Any(e => e.İnformationFormId == id)).GetValueOrDefault();
        }
    }
}
