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
         
        // GET: AboutPages
        public async Task<IActionResult> Index()
        {
              return _context.aboutPages != null ? 
                          View(await _context.aboutPages.ToListAsync()) :
                          Problem("Entity set 'Context.aboutPages'  is null.");
        }
        //[Authorize]
        // GET: AboutPages/Details/5
        public async Task<IActionResult> Details(int? id)
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
        //[Authorize]
        // GET: AboutPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutId,ImageHeader,ImageBottom,ImageTop,CompanyDescription,MissionTitle,MissionDescription,MissionBgColor,VisionTitle,VisionDescription,VisionBgColor,WhyUsSectionTitle,WhyUsSectionBgColor,WhyUs1Title,WhyUs1Description,WhyUs1BgColor,WhyUs2Title,WhyUs2Description,WhyUs2BgColor,WhyUs3Title,WhyUs3Description,WhyUs3BgColor")] AboutPage aboutPage, IFormFile imageFile)
        {
           
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                    // File path where the image will be saved (in the "Image" folder)
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Image", fileName);

                    // Copy the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Set the ImageHeader, ImageBottom, or other appropriate property to the file name
                    aboutPage.ImageHeader = fileName;
                    aboutPage.ImageBottom = fileName;
                    aboutPage.ImageTop = fileName;
                    // Repeat the above line for other image properties if needed
                }

                // Add the aboutPage to the context
                _context.Add(aboutPage);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AboutId,ImageHeader,ImageBottom,ImageTop,CompanyDescription,MissionTitle,MissionDescription,MissionBgColor,VisionTitle,VisionDescription,VisionBgColor,WhyUsSectionTitle,WhyUsSectionBgColor,WhyUs1Title,WhyUs1Description,WhyUs1BgColor,WhyUs2Title,WhyUs2Description,WhyUs2BgColor,WhyUs3Title,WhyUs3Description,WhyUs3BgColor")] AboutPage aboutPage)
        {
            if (id != aboutPage.AboutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutPage);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(aboutPage);
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
                return Problem("Entity set 'Context.aboutPages'  is null.");
            }
            var aboutPage = await _context.aboutPages.FindAsync(id);
            if (aboutPage != null)
            {
                _context.aboutPages.Remove(aboutPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutPageExists(int id)
        {
          return (_context.aboutPages?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }
    }
}
