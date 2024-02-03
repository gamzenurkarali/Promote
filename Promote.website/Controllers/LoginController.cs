using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using Promote.website.Services;

namespace Promote.website.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;
        private readonly LayoutService _layoutService;
        public LoginController(Context context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
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
                HttpContext.Session.SetString("UserId", adminuserinfo.AdminId.ToString());//HttpContext.Session.GetString("UserId")
                return RedirectToAction("Router", "AboutPages");
                
            }
            else
            {
                TempData["Message"] = "Hatalı giriş!";
                return View("Index");
            }
        }

        public IActionResult Logout()
        { 
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Login");
        }
    }
}
