using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class BaseController : Controller
    {
        private readonly Context _context;

        public BaseController(Context context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            Layout layoutData = _context.layouts.FirstOrDefault();
             
            if (layoutData != null)
            {
                ViewData["Layout"] = layoutData;
            }
            else
            {
                // Layout verisi null ise, gerekli işlemleri yapabilirsiniz.
                // Örneğin, bir hata mesajı gösterebilir veya varsayılan bir değer atayabilirsiniz.
            }
             
            List<Sublink> sublinkData = _context.sublinks.ToList();
             
            if (sublinkData != null)
            {
                ViewData["Sublinks"] = sublinkData;
            }
            else
            {
                // Sublink verisi null ise, gerekli işlemleri yapabilirsiniz.
                // Örneğin, bir hata mesajı gösterebilir veya varsayılan bir değer atayabilirsiniz.
            }
             
            base.OnActionExecuting(filterContext);
        }
    }
}
