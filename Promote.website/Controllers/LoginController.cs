using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
         
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(Admin p)
        {
            var adminuserinfo = _context.admins?.FirstOrDefault(x => x.UserName == p.UserName && x.Password == p.Password);

            if (adminuserinfo != null)
            {
                return RedirectToAction("Index", "AboutPages");
            }
            else
            {
                TempData["Message"] = "Hatalı giriş!";
                return View("Index");
            }
        }

    }
}
