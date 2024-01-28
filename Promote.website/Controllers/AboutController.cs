using Microsoft.AspNetCore.Mvc;

namespace Promote.website.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
