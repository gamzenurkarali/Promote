using Microsoft.AspNetCore.Mvc;

namespace Promote.website.Controllers
{
    public class ToursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
