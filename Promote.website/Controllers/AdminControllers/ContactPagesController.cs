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
    public class ContactPagesController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContactPagesController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Router()
        {
            // Veritabanında AboutPages tablosunda kayıt var mı diye kontrol et
            bool hasRecord = _context.contactPages.Any();

            // Eğer bir kayıt varsa, ilk kaydın ID'sini al
            int firstRecordId = hasRecord ? _context.contactPages.First().ContactPageId : 0;

            if (hasRecord)
            {
                // Eğer bir kayıt varsa, Edit action'ına yönlendir
                return RedirectToAction("Edit", new { id = firstRecordId });
            }
            else
            {
                // Eğer kayıt yoksa, Create action'ına yönlendir
                return RedirectToAction("Create");
            }
        }
        
        //[Authorize]
        // GET: ContactPages/Create
        public IActionResult Create()
        {
            // Veritabanında AboutPages tablosunda kayıt var mı diye kontrol et
            bool hasRecord = _context.contactPages.Any();

            // Eğer bir kayıt varsa, ilk kaydın ID'sini al
            int firstRecordId = hasRecord ? _context.contactPages.First().ContactPageId : 0;

            if (hasRecord)
            {
                // Eğer bir kayıt varsa, Edit action'ına yönlendir
                return RedirectToAction("Edit", new { id = firstRecordId });
            }
            else
            {
                return View();
            }
            
        }

        // POST: ContactPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactPageId,ImageHeader,ContactInfoTitle,ContactInfoDescription,PhoneNumber,EmailAddress,MapIframeUrl")] ContactPage contactPage, IFormFile headerImage)
        {
            if (ModelState.IsValid)
            {
                if (headerImage != null && headerImage.Length > 0)
                {
                    string fileName = headerImage.FileName;
                    // Dosyanın kaydedileceği yol (image klasörü)
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Image", fileName);

                    // Dosyayı kopyala
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await headerImage.CopyToAsync(stream);
                    }

                    // Set the ImageHeader property to the file name
                    contactPage.ImageHeader = fileName;
                }

                _context.Add(contactPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactPage);
        }

        //[Authorize]
        // GET: ContactPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.contactPages == null)
            {
                return NotFound();
            }

            var contactPage = await _context.contactPages.FindAsync(id);
            if (contactPage == null)
            {
                return NotFound();
            }
            return View(contactPage);
        }

        // POST: ContactPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactPageId,ImageHeader,ContactInfoTitle,ContactInfoDescription,PhoneNumber,EmailAddress,MapIframeUrl")] ContactPage contactPage)
        {
            if (id != contactPage.ContactPageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactPageExists(contactPage.ContactPageId))
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
            return View(contactPage);
        }
        //[Authorize]
        // GET: ContactPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.contactPages == null)
            {
                return NotFound();
            }

            var contactPage = await _context.contactPages
                .FirstOrDefaultAsync(m => m.ContactPageId == id);
            if (contactPage == null)
            {
                return NotFound();
            }

            return View(contactPage);
        }

        // POST: ContactPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.contactPages == null)
            {
                return Problem("Entity set 'Context.contactPages'  is null.");
            }
            var contactPage = await _context.contactPages.FindAsync(id);
            if (contactPage != null)
            {
                _context.contactPages.Remove(contactPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPageExists(int id)
        {
          return (_context.contactPages?.Any(e => e.ContactPageId == id)).GetValueOrDefault();
        }
    }
}
