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
    public class ContactFormsController : Controller
    {
        private readonly Context _context;

        public ContactFormsController(Context context)
        {
            _context = context;
        }
        [Authorize]
        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
              return _context.contactForms != null ? 
                          View(await _context.contactForms.ToListAsync()) :
                          Problem("Entity set 'Context.contactForms'  is null.");
        }
        [Authorize]
        // GET: ContactForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.contactForms == null)
            {
                return NotFound();
            }

            var contactForm = await _context.contactForms
                .FirstOrDefaultAsync(m => m.ContactFormId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }
        [Authorize]
        // GET: ContactForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactFormId,FirstName,LastName,Email,Message")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactForm);
        }
        [Authorize]
        // GET: ContactForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.contactForms == null)
            {
                return NotFound();
            }

            var contactForm = await _context.contactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactFormId,FirstName,LastName,Email,Message")] ContactForm contactForm)
        {
            if (id != contactForm.ContactFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactFormExists(contactForm.ContactFormId))
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
            return View(contactForm);
        }
        [Authorize]
        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.contactForms == null)
            {
                return NotFound();
            }

            var contactForm = await _context.contactForms
                .FirstOrDefaultAsync(m => m.ContactFormId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.contactForms == null)
            {
                return Problem("Entity set 'Context.contactForms'  is null.");
            }
            var contactForm = await _context.contactForms.FindAsync(id);
            if (contactForm != null)
            {
                _context.contactForms.Remove(contactForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
          return (_context.contactForms?.Any(e => e.ContactFormId == id)).GetValueOrDefault();
        }
    }
}
