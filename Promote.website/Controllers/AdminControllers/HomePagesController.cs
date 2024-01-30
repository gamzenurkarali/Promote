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
    public class HomePagesController : Controller
    {
        private readonly Context _context;

        public HomePagesController(Context context)
        {
            _context = context;
        }
        //[Authorize]
        // GET: HomePages
        public async Task<IActionResult> Index()
        {
              return _context.homePages != null ? 
                          View(await _context.homePages.ToListAsync()) :
                          Problem("Entity set 'Context.homePages'  is null.");
        }
        //[Authorize]
        // GET: HomePages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.homePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.homePages
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }
        //[Authorize]
        // GET: HomePages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomePages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeId,VideoFileName,IsTaglineSectionIncluded,TaglineSectionBgColor,Tagline,IsPopularProductsSectionIncluded,PopularProductsSectionTitle,PopularProductsSectionBgColor,PopularProduct1Title,PopularProduct1Image,PopularProduct1Id,PopularProduct2Title,PopularProduct2Image,PopularProduct2Id,PopularProduct3Title,PopularProduct3Image,PopularProduct3Id,PopularProduct4Title,PopularProduct4Image,PopularProduct4Id,IsServicesSectionIncluded,ServicesSectionTitle,ServicesSectionBgColor,Services1Image,Services1Description,Services2Image,Services2Description,Services3Image,Services3Description,Services4Image,Services4Description,IsStatisticsSectionIncluded,StatisticSectionBgColor,Statistic1Number,Statistic1Title,Statistic2Number,Statistic2Title,Statistic3Number,Statistic3Title,Statistic4Number,Statistic4Title")] HomePage homePage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homePage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homePage);
        }
        //[Authorize]
        // GET: HomePages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.homePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.homePages.FindAsync(id);
            if (homePage == null)
            {
                return NotFound();
            }
            return View(homePage);
        }

        // POST: HomePages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HomeId,VideoFileName,IsTaglineSectionIncluded,TaglineSectionBgColor,Tagline,IsPopularProductsSectionIncluded,PopularProductsSectionTitle,PopularProductsSectionBgColor,PopularProduct1Title,PopularProduct1Image,PopularProduct1Id,PopularProduct2Title,PopularProduct2Image,PopularProduct2Id,PopularProduct3Title,PopularProduct3Image,PopularProduct3Id,PopularProduct4Title,PopularProduct4Image,PopularProduct4Id,IsServicesSectionIncluded,ServicesSectionTitle,ServicesSectionBgColor,Services1Image,Services1Description,Services2Image,Services2Description,Services3Image,Services3Description,Services4Image,Services4Description,IsStatisticsSectionIncluded,StatisticSectionBgColor,Statistic1Number,Statistic1Title,Statistic2Number,Statistic2Title,Statistic3Number,Statistic3Title,Statistic4Number,Statistic4Title")] HomePage homePage)
        {
            if (id != homePage.HomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageExists(homePage.HomeId))
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
            return View(homePage);
        }
        //[Authorize]
        // GET: HomePages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.homePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.homePages
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }

        // POST: HomePages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.homePages == null)
            {
                return Problem("Entity set 'Context.homePages'  is null.");
            }
            var homePage = await _context.homePages.FindAsync(id);
            if (homePage != null)
            {
                _context.homePages.Remove(homePage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomePageExists(int id)
        {
          return (_context.homePages?.Any(e => e.HomeId == id)).GetValueOrDefault();
        }
    }
}
