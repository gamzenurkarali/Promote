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
    //[Authorize]
    public class AboutPagesController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AboutPagesController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Router()
        {
            // Veritabanında AboutPages tablosunda kayıt var mı diye kontrol et
            bool hasRecord = _context.aboutPages.Any();

            // Eğer bir kayıt varsa, ilk kaydın ID'sini al
            int firstRecordId = hasRecord ? _context.aboutPages.First().AboutId : 0;

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
       
        // GET: AboutPages/Create
        public IActionResult Create()
        { 
            bool hasRecord = _context.aboutPages.Any();
             
            int firstRecordId = hasRecord ? _context.aboutPages.First().AboutId : 0;

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

        // POST: AboutPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Create(IFormFile ImageHeader, IFormFile ImageBottom, IFormFile ImageTop, [Bind("CompanyDescription,MissionTitle,MissionDescription,MissionBgColor,VisionTitle,VisionDescription,VisionBgColor,WhyUsSectionTitle,WhyUsSectionBgColor,WhyUs1Title,WhyUs1Description,WhyUs1BgColor,WhyUs2Title,WhyUs2Description,WhyUs2BgColor,WhyUs3Title,WhyUs3Description,WhyUs3BgColor")] AboutPage aboutPage)
        {
            try
            {
                if (ImageHeader != null && ImageTop != null && ImageBottom != null
                    && !string.IsNullOrEmpty(aboutPage.CompanyDescription)
                    && !string.IsNullOrEmpty(aboutPage.MissionTitle)
                    && !string.IsNullOrEmpty(aboutPage.MissionDescription)
                    && !string.IsNullOrEmpty(aboutPage.MissionBgColor)
                    && !string.IsNullOrEmpty(aboutPage.VisionTitle)
                    && !string.IsNullOrEmpty(aboutPage.VisionDescription)
                    && !string.IsNullOrEmpty(aboutPage.VisionBgColor)
                    && !string.IsNullOrEmpty(aboutPage.WhyUsSectionTitle)
                    && !string.IsNullOrEmpty(aboutPage.WhyUsSectionBgColor)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs1Title)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs1Description)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs1BgColor)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs2Title)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs2Description)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs2BgColor)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs3Title)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs3Description)
                    && !string.IsNullOrEmpty(aboutPage.WhyUs3BgColor))
                {
                    string fileNameHeader = Guid.NewGuid().ToString() + Path.GetExtension(ImageHeader.FileName);
                    string fileNameTop = Guid.NewGuid().ToString() + Path.GetExtension(ImageTop.FileName);
                    string fileNameBottom = Guid.NewGuid().ToString() + Path.GetExtension(ImageBottom.FileName);
                     
                    string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameHeader);
                    string filePathTop = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameTop);
                    string filePathBottom = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameBottom);

                    using (var stream = new FileStream(filePathHeader, FileMode.Create))
                    {
                        await ImageHeader.CopyToAsync(stream);
                    }

                    using (var stream = new FileStream(filePathTop, FileMode.Create))
                    {
                        await ImageTop.CopyToAsync(stream);
                    }

                    using (var stream = new FileStream(filePathBottom, FileMode.Create))
                    {
                        await ImageBottom.CopyToAsync(stream);
                    }
                     
                    aboutPage.ImageHeader = fileNameHeader;
                    aboutPage.ImageBottom = fileNameBottom;
                    aboutPage.ImageTop = fileNameTop;

                    _context.Add(aboutPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "AboutPage successfully created!";
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
        // GET: AboutPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.aboutPages == null)
            {
                return NotFound();
            }

            var aboutPage = await _context.aboutPages.FindAsync(id);
            if (aboutPage == null)
            {
                return NotFound();
            }
            return View(aboutPage);
        }
        // POST: AboutPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)]       //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Edit(int id, IFormFile ImageHeader, IFormFile ImageBottom, IFormFile ImageTop, [Bind("AboutId,CompanyDescription,MissionTitle,MissionDescription,MissionBgColor,VisionTitle,VisionDescription,VisionBgColor,WhyUsSectionTitle,WhyUsSectionBgColor,WhyUs1Title,WhyUs1Description,WhyUs1BgColor,WhyUs2Title,WhyUs2Description,WhyUs2BgColor,WhyUs3Title,WhyUs3Description,WhyUs3BgColor")] AboutPage aboutPage)
        {
            if (id != aboutPage.AboutId)
            {
                return NotFound();
            }


            try
            {
                var existingAboutPage = await _context.aboutPages.AsNoTracking().FirstOrDefaultAsync(m => m.AboutId == id);



                if (aboutPage.CompanyDescription != null
                     && aboutPage.MissionTitle != null
                     && aboutPage.MissionDescription != null
                     && aboutPage.MissionBgColor != null
                     && aboutPage.VisionTitle != null
                     && aboutPage.VisionDescription != null
                     && aboutPage.VisionBgColor != null
                     && aboutPage.WhyUsSectionTitle != null
                     && aboutPage.WhyUsSectionBgColor != null
                     && aboutPage.WhyUs1Title != null
                     && aboutPage.WhyUs1Description != null
                     && aboutPage.WhyUs1BgColor != null
                     && aboutPage.WhyUs2Title != null
                     && aboutPage.WhyUs2Description != null
                     && aboutPage.WhyUs2BgColor != null
                     && aboutPage.WhyUs3Title != null
                     && aboutPage.WhyUs3Description != null
                     && aboutPage.WhyUs3BgColor != null)
                {
                    if (ImageHeader != null && existingAboutPage != null && !string.IsNullOrEmpty(existingAboutPage.ImageHeader) && ImageHeader.FileName != existingAboutPage.ImageHeader)
                    {
                        string filePathHeader = Path.Combine(_hostingEnvironment.WebRootPath, "Media", existingAboutPage.ImageHeader);
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

                        aboutPage.ImageHeader = fileNameHeader;
                    }
                    else
                    {
                        aboutPage.ImageHeader = existingAboutPage.ImageHeader;
                    }
                    if (ImageBottom != null && existingAboutPage != null && !string.IsNullOrEmpty(existingAboutPage.ImageBottom) && ImageBottom.FileName != existingAboutPage.ImageBottom)
                    {
                        string filePathBottom = Path.Combine(_hostingEnvironment.WebRootPath, "Media", existingAboutPage.ImageBottom);
                        if (System.IO.File.Exists(filePathBottom))
                        {
                            System.IO.File.Delete(filePathBottom);
                        }
                    }

                    if (ImageBottom != null)
                    {
                        string fileNameBottom = Guid.NewGuid().ToString() + Path.GetExtension(ImageBottom.FileName);
                        string filePathBottom = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameBottom);

                        using (var stream = new FileStream(filePathBottom, FileMode.Create))
                        {
                            await ImageBottom.CopyToAsync(stream);
                        }

                        aboutPage.ImageBottom = fileNameBottom;
                    }
                    else
                    {
                        aboutPage.ImageBottom = existingAboutPage.ImageBottom;
                    }
                    if (ImageTop != null && existingAboutPage != null && !string.IsNullOrEmpty(existingAboutPage.ImageTop) && ImageTop.FileName != existingAboutPage.ImageTop)
                    {
                        string filePathTop = Path.Combine(_hostingEnvironment.WebRootPath, "Media", existingAboutPage.ImageTop);
                        if (System.IO.File.Exists(filePathTop))
                        {
                            System.IO.File.Delete(filePathTop);
                        }
                    }

                    if (ImageTop != null)
                    {
                        string fileNameTop = Guid.NewGuid().ToString() + Path.GetExtension(ImageTop.FileName);
                        string filePathTop = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileNameTop);

                        using (var stream = new FileStream(filePathTop, FileMode.Create))
                        {
                            await ImageTop.CopyToAsync(stream);
                        }

                        aboutPage.ImageTop = fileNameTop;
                    }
                    else
                    {
                        aboutPage.ImageTop = existingAboutPage.ImageTop;
                    }
                    _context.Update(aboutPage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "About page updated successfully!";
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
                if (!AboutPageExists(aboutPage.AboutId))
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
        // GET: AboutPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.aboutPages == null)
            {
                return NotFound();
            }

            var aboutPage = await _context.aboutPages
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (aboutPage == null)
            {
                return NotFound();
            }

            return View(aboutPage);
        }

        // POST: AboutPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.aboutPages == null)
            {
                return Problem("Entity set 'Context.aboutPages' is null.");
            }

            var aboutPage = await _context.aboutPages.FindAsync(id);

            if (aboutPage != null)
            { 
                string imageDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "Media");
                 
                if (!string.IsNullOrEmpty(aboutPage.ImageHeader))
                {
                    string filePathHeader = Path.Combine(imageDirectory, aboutPage.ImageHeader);
                    if (System.IO.File.Exists(filePathHeader))
                    {
                        System.IO.File.Delete(filePathHeader);
                    }
                }

                if (!string.IsNullOrEmpty(aboutPage.ImageTop))
                {
                    string filePathTop = Path.Combine(imageDirectory, aboutPage.ImageTop);
                    if (System.IO.File.Exists(filePathTop))
                    {
                        System.IO.File.Delete(filePathTop);
                    }
                }

                if (!string.IsNullOrEmpty(aboutPage.ImageBottom))
                {
                    string filePathBottom = Path.Combine(imageDirectory, aboutPage.ImageBottom);
                    if (System.IO.File.Exists(filePathBottom))
                    {
                        System.IO.File.Delete(filePathBottom);
                    }
                }

                // Veritabanından kaydı silme işlemi
                _context.aboutPages.Remove(aboutPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool AboutPageExists(int id)
        {
          return (_context.aboutPages?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }
    }
}
