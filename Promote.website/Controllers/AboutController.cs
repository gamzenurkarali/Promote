using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class AboutController : Controller
    {
        private readonly Context _context;

        public AboutController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.aboutPages.ToList(); // AboutPage modelinin DbSet'i üzerinden verileri çekiyoruz
            return View(values);
        }
    }
}

