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
    public class InformationFormsController : Controller
    {
        private readonly Context _context;

        public InformationFormsController(Context context)
        {
            _context = context;
        }

        // GET: InformationForms
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.informationForms != null ? 
                          View(await _context.informationForms.ToListAsync()) :
                          Problem("Entity set 'Context.informationForms'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }

        }

        // GET: InformationForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.informationForms == null)
            {
                return NotFound();
            }

            var informationForm = await _context.informationForms
                .FirstOrDefaultAsync(m => m.InformationFormId == id);
            if (informationForm == null)
            {
                return NotFound();
            }

            return View(informationForm);
        }

        // GET: InformationForms/Create
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

            // POST: InformationForms/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformationFormId,EmailAdress")] InformationForm informationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationForm);
        }

        // GET: InformationForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.informationForms == null)
                {
                    return NotFound();
                }

                var informationForm = await _context.informationForms.FindAsync(id);
                if (informationForm == null)
                {
                    return NotFound();
                }
                return View(informationForm);
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

            // POST: InformationForms/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformationFormId,EmailAdress")] InformationForm informationForm)
        {
            if (id != informationForm.InformationFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationFormExists(informationForm.InformationFormId))
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
            return View(informationForm);
        }

        // GET: InformationForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.informationForms == null)
                {
                    return NotFound();
                }

                var informationForm = await _context.informationForms
                    .FirstOrDefaultAsync(m => m.InformationFormId == id);
                if (informationForm == null)
                {
                    return NotFound();
                }

                return View(informationForm);
            }
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

    // POST: InformationForms/Delete/5
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.informationForms == null)
            {
                return Problem("Entity set 'Context.informationForms'  is null.");
            }
            var informationForm = await _context.informationForms.FindAsync(id);
            if (informationForm != null)
            {
                _context.informationForms.Remove(informationForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationFormExists(int id)
        {
          return (_context.informationForms?.Any(e => e.InformationFormId == id)).GetValueOrDefault();
        }
    }
}
