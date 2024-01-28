using Microsoft.AspNetCore.Mvc;

namespace Promote.website.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
