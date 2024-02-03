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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomePagesController(Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Router()
        {
            bool hasRecord = _context.homePages.Any();

            int firstRecordId = hasRecord ? _context.homePages.First().HomeId : 0;

            if (hasRecord)
            {
                return RedirectToAction("Edit", new { id = firstRecordId });
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        //[Authorize]
        // GET: HomePages/Create
        public IActionResult Create()
        {
            // Veritabanında AboutPages tablosunda kayıt var mı diye kontrol et
            bool hasRecord = _context.homePages.Any();

            // Eğer bir kayıt varsa, ilk kaydın ID'sini al
            int firstRecordId = hasRecord ? _context.homePages.First().HomeId : 0;

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

        // POST: HomePages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Create(
     IFormFile VideoFileName,
     IFormFile PopularProduct1Image,
     IFormFile PopularProduct2Image,
     IFormFile PopularProduct3Image,
     IFormFile PopularProduct4Image,
     IFormFile Services1Image,
     IFormFile Services2Image,
     IFormFile Services3Image,
     IFormFile Services4Image,
     [Bind("VideoFileName,IsTaglineSectionIncluded,TaglineSectionBgColor,Tagline,IsPopularProductsSectionIncluded,PopularProductsSectionTitle,PopularProductsSectionBgColor,PopularProduct1Title,PopularProduct1Id,PopularProduct2Title,PopularProduct2Id,PopularProduct3Title,PopularProduct3Id,PopularProduct4Title,PopularProduct4Id,IsServicesSectionIncluded,ServicesSectionTitle,ServicesSectionBgColor,Services1Description,Services2Description,Services3Description,Services4Description,IsStatisticsSectionIncluded,StatisticSectionBgColor,Statistic1Number,Statistic1Title,Statistic2Number,Statistic2Title,Statistic3Number,Statistic3Title,Statistic4Number,Statistic4Title")] HomePage homePage)
        {
            try
            {
                if (VideoFileName != null &&
                    PopularProduct1Image != null &&
                    PopularProduct2Image != null &&
                    PopularProduct3Image != null &&
                    PopularProduct4Image != null &&
                    Services1Image != null &&
                    Services2Image != null &&
                    Services3Image != null &&
                    Services4Image != null &&
                    !string.IsNullOrEmpty(homePage.PopularProduct1Title) &&
                    !string.IsNullOrEmpty(homePage.PopularProduct2Title) &&
                    !string.IsNullOrEmpty(homePage.PopularProduct3Title) &&
                    !string.IsNullOrEmpty(homePage.PopularProduct4Title) &&
                    !string.IsNullOrEmpty(homePage.Services1Description) &&
                    !string.IsNullOrEmpty(homePage.Services2Description) &&
                    !string.IsNullOrEmpty(homePage.Services3Description) &&
                    !string.IsNullOrEmpty(homePage.Services4Description) &&
                    !string.IsNullOrEmpty(homePage.Statistic1Title) &&
                    !string.IsNullOrEmpty(homePage.Statistic2Title) &&
                    !string.IsNullOrEmpty(homePage.Statistic3Title) &&
                    !string.IsNullOrEmpty(homePage.Statistic4Title))
                {
                    homePage.VideoFileName = await SaveFile(VideoFileName);
                    homePage.PopularProduct1Image = await SaveFile(PopularProduct1Image);
                    homePage.PopularProduct2Image = await SaveFile(PopularProduct2Image);
                    homePage.PopularProduct3Image = await SaveFile(PopularProduct3Image);
                    homePage.PopularProduct4Image = await SaveFile(PopularProduct4Image);
                    homePage.Services1Image = await SaveFile(Services1Image);
                    homePage.Services2Image = await SaveFile(Services2Image);
                    homePage.Services3Image = await SaveFile(Services3Image);
                    homePage.Services4Image = await SaveFile(Services4Image);

                    _context.Add(homePage);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "HomePage successfully created!";
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

            return View(homePage);
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
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
        [RequestSizeLimit(500 * 1024 * 1024)] // 500 MB limit
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Edit(int id,
    IFormFile VideoFileName,
    IFormFile PopularProduct1Image,
    IFormFile PopularProduct2Image,
    IFormFile PopularProduct3Image,
    IFormFile PopularProduct4Image,
    IFormFile Services1Image,
    IFormFile Services2Image,
    IFormFile Services3Image,
    IFormFile Services4Image,
    [Bind("HomeId,VideoFileName,IsTaglineSectionIncluded,TaglineSectionBgColor,Tagline,IsPopularProductsSectionIncluded,PopularProductsSectionTitle,PopularProductsSectionBgColor,PopularProduct1Title,PopularProduct1Id,PopularProduct2Title,PopularProduct2Id,PopularProduct3Title,PopularProduct3Id,PopularProduct4Title,PopularProduct4Id,IsServicesSectionIncluded,ServicesSectionTitle,ServicesSectionBgColor,Services1Description,Services2Description,Services3Description,Services4Description,IsStatisticsSectionIncluded,StatisticSectionBgColor,Statistic1Number,Statistic1Title,Statistic2Number,Statistic2Title,Statistic3Number,Statistic3Title,Statistic4Number,Statistic4Title")] HomePage homePage)
        {
            if (id != homePage.HomeId)
            {
                return NotFound();
            }


            try
            {
                var existingHomePage = await _context.homePages.AsNoTracking().FirstOrDefaultAsync(m => m.HomeId == id);


                if (homePage.TaglineSectionBgColor == null
                    || homePage.Tagline == null
                    || homePage.PopularProductsSectionTitle == null
                    || homePage.PopularProductsSectionBgColor == null
                    || homePage.PopularProduct1Title == null
                    || homePage.PopularProduct1Id == null
                    || homePage.PopularProduct2Title == null
                    || homePage.PopularProduct2Id == null
                    || homePage.PopularProduct3Title == null
                    || homePage.PopularProduct3Id == null
                    || homePage.PopularProduct4Title == null
                    || homePage.PopularProduct4Id == null
                    || homePage.ServicesSectionTitle == null
                    || homePage.ServicesSectionBgColor == null
                    || homePage.Services1Description == null
                    || homePage.Services2Description == null
                    || homePage.Services3Description == null
                    || homePage.Services4Description == null
                    || homePage.StatisticSectionBgColor == null
                    || homePage.Statistic1Number == null
                    || homePage.Statistic1Title == null
                    || homePage.Statistic2Number == null
                    || homePage.Statistic2Title == null
                    || homePage.Statistic3Number == null
                    || homePage.Statistic3Title == null
                    || homePage.Statistic4Number == null
                    || homePage.Statistic4Title == null)
                {
                    TempData["Message"] = "Please fill in all fields.";
                    TempData["AlertClass"] = "alert-danger";
                    return RedirectToAction("Router");
                }
                else
                {
                    if (VideoFileName != null)
                    {
                        await DeleteFileIfExists(existingHomePage.VideoFileName);
                    }

                    if (PopularProduct1Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.PopularProduct1Image);
                    }

                    if (PopularProduct2Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.PopularProduct2Image);
                    }

                    if (PopularProduct3Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.PopularProduct3Image);
                    }

                    if (PopularProduct4Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.PopularProduct4Image);
                    }

                    if (Services1Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.Services1Image);
                    }

                    if (Services2Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.Services2Image);
                    }

                    if (Services3Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.Services3Image);
                    }

                    if (Services4Image != null)
                    {
                        await DeleteFileIfExists(existingHomePage.Services4Image);
                    }

                     
                    homePage.VideoFileName = VideoFileName != null ? await SaveFile(VideoFileName) : existingHomePage.VideoFileName;
                    homePage.PopularProduct1Image = PopularProduct1Image != null ? await SaveFile(PopularProduct1Image) : existingHomePage.PopularProduct1Image;
                    homePage.PopularProduct2Image = PopularProduct2Image != null ? await SaveFile(PopularProduct2Image) : existingHomePage.PopularProduct2Image;
                    homePage.PopularProduct3Image = PopularProduct3Image != null ? await SaveFile(PopularProduct3Image) : existingHomePage.PopularProduct3Image;
                    homePage.PopularProduct4Image = PopularProduct4Image != null ? await SaveFile(PopularProduct4Image) : existingHomePage.PopularProduct4Image;
                    homePage.Services1Image = Services1Image != null ? await SaveFile(Services1Image) : existingHomePage.Services1Image;
                    homePage.Services2Image = Services2Image != null ? await SaveFile(Services2Image) : existingHomePage.Services2Image;
                    homePage.Services3Image = Services3Image != null ? await SaveFile(Services3Image) : existingHomePage.Services3Image;
                    homePage.Services4Image = Services4Image != null ? await SaveFile(Services4Image) : existingHomePage.Services4Image;

                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Home page updated successfully!";
                    TempData["AlertClass"] = "alert-success";
                }
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
            return RedirectToAction("Router");

        }

        private async Task DeleteFileIfExists(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Media", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
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
                // Dosyaların silme işlemleri
                await DeleteFileIfExists(homePage.VideoFileName);
                await DeleteFileIfExists(homePage.PopularProduct1Image);
                await DeleteFileIfExists(homePage.PopularProduct2Image);
                await DeleteFileIfExists(homePage.PopularProduct3Image);
                await DeleteFileIfExists(homePage.PopularProduct4Image);
                await DeleteFileIfExists(homePage.Services1Image);
                await DeleteFileIfExists(homePage.Services2Image);
                await DeleteFileIfExists(homePage.Services3Image);
                await DeleteFileIfExists(homePage.Services4Image);

                _context.homePages.Remove(homePage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool HomePageExists(int id)
        {
            return (_context.homePages?.Any(e => e.HomeId == id)).GetValueOrDefault();
        }
    }
}
