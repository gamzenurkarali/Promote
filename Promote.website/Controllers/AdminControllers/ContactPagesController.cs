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
            if (HttpContext.Session.GetString("UserId") != null)
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
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }

        }

        // POST: ContactPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(500 * 1024 * 1024)] // unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Create([Bind("ContactPageId,ImageHeader,ContactInfoTitle,ContactInfoDescription,PhoneNumber,EmailAddress,MapIframeUrl")] ContactPage contactPage, IFormFile ImageHeader)
        {
            try
            {
                if (ImageHeader != null
                    && !string.IsNullOrEmpty(contactPage.ContactInfoTitle)
                    && !string.IsNullOrEmpty(contactPage.ContactInfoDescription)
                    && !string.IsNullOrEmpty(contactPage.PhoneNumber)
                    && !string.IsNullOrEmpty(contactPage.EmailAddress)
                    && !string.IsNullOrEmpty(contactPage.MapIframeUrl))
                {
                    string fileNameHeader = Guid.NewGuid().ToString() + Path.GetExtension(ImageHeader.FileName);
                    string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameHeader);

                    using (var stream = new FileStream(filePathHeader, FileMode.Create))
                    {
                        await ImageHeader.CopyToAsync(stream);
                    }

                    contactPage.ImageHeader = fileNameHeader;

                    _context.Add(contactPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "ContactPage successfully created!";
                    TempData["AlertClass"] = "alert-success";
                    return RedirectToAction("Router");
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

            return RedirectToAction("Router");
        }



        //[Authorize]
        // GET: ContactPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
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
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

        // POST: ContactPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(500 * 1024 * 1024)] //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Edit(int id, IFormFile ImageHeader,[Bind("ContactPageId,ImageHeader,ContactInfoTitle,ContactInfoDescription,PhoneNumber,EmailAddress,MapIframeUrl")] ContactPage contactPage)
        {
            if (id != contactPage.ContactPageId)
            {
                return NotFound();
            }

            
            try
            {
                var existingContactPage = await _context.contactPages.AsNoTracking().FirstOrDefaultAsync(m => m.ContactPageId == id);

                if (contactPage.ContactInfoTitle != null
                     && contactPage.ContactInfoDescription != null
                     && contactPage.PhoneNumber != null
                     && contactPage.EmailAddress != null
                     && contactPage.MapIframeUrl != null)
                {


                    // Eski dosyaları silme işlemleri
                    if (ImageHeader != null && existingContactPage != null && !string.IsNullOrEmpty(existingContactPage.ImageHeader) && ImageHeader.FileName != existingContactPage.ImageHeader)
                    {
                        string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", existingContactPage.ImageHeader);
                        if (System.IO.File.Exists(filePathHeader))
                        {
                            System.IO.File.Delete(filePathHeader);
                        }
                    }

                    if (ImageHeader != null)
                    {
                        string fileNameHeader = Guid.NewGuid().ToString() + Path.GetExtension(ImageHeader.FileName);
                        string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameHeader);

                        using (var stream = new FileStream(filePathHeader, FileMode.Create))
                        {
                            await ImageHeader.CopyToAsync(stream);
                        }

                        contactPage.ImageHeader = fileNameHeader;
                    }
                    else
                    {
                        contactPage.ImageHeader = existingContactPage.ImageHeader;
                    }
                    _context.Update(contactPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Contact page updated successfully!";
                    TempData["AlertClass"] = "alert-success";
                    return RedirectToAction("Router");
                }
                else
                {
                    TempData["Message"] = "Please fill in all field.";
                    TempData["AlertClass"] = "alert-danger";
                    return RedirectToAction("Router");
                }

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


        }


        //[Authorize]
        // GET: ContactPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
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
            else
            {
                return RedirectToAction("Index", "LoginAdminPanel100224cr");
            }
        }

        // POST: ContactPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var contactPage = await _context.contactPages.FindAsync(id);

                if (contactPage == null)
                {
                    TempData["Message"] = "ContactPage not found!";
                    TempData["AlertClass"] = "alert-danger";
                    return RedirectToAction("Router");
                }

                // Dosyanın fiziksel olarak silinmesi (isteğe bağlı)
                string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", contactPage.ImageHeader);
                if (System.IO.File.Exists(filePathHeader))
                {
                    System.IO.File.Delete(filePathHeader);
                }

                _context.contactPages.Remove(contactPage);
                await _context.SaveChangesAsync();

                TempData["Message"] = "ContactPage successfully deleted!";
                TempData["AlertClass"] = "alert-success";
                return RedirectToAction("Router");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An error occurred: {ex.Message}";
                TempData["AlertClass"] = "alert-danger";
            }

            return RedirectToAction("Router");
        }
        private bool ContactPageExists(int id)
        {
            return _context.contactPages.Any(e => e.ContactPageId == id);
        }
    }
}
