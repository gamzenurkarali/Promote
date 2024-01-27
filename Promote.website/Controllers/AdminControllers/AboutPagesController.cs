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
    public class AboutPagesController : Controller
    {
        private readonly Context _context;

        public AboutPagesController(Context context)
        {
            _context = context;
        }
         
        // GET: AboutPages
        public async Task<IActionResult> Index()
        {
              return _context.aboutPages != null ? 
                          View(await _context.aboutPages.ToListAsync()) :
                          Problem("Entity set 'Context.aboutPages'  is null.");
        }
        [Authorize]
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
        [Authorize]
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
        public async Task<IActionResult> Create([Bind("AboutId,ImageHeader,ImageBottom,ImageTop,CompanyDescription,MissionTitle,MissionDescription,MissionBgColor,VisionTitle,VisionDescription,VisionBgColor,WhyUsSectionTitle,WhyUsSectionBgColor,WhyUs1Title,WhyUs1Description,WhyUs1BgColor,WhyUs2Title,WhyUs2Description,WhyUs2BgColor,WhyUs3Title,WhyUs3Description,WhyUs3BgColor")] AboutPage aboutPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutPage);
        }
        [Authorize]
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
        [Authorize]
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
