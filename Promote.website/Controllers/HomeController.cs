using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using Promote.website.Services;
using System.Diagnostics;

namespace Promote.website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        private readonly LayoutService _layoutService;
        public HomeController(ILogger<HomeController> logger, Context context, LayoutService layoutService)
        {
            _logger = logger;
            _context = context;
            _layoutService = layoutService;
        }

        public IActionResult Index(int informationFormId)
        {
            var documents = _layoutService.GetDocuments();
            ViewBag.relDocuments = documents;
            var layout = _layoutService.GetLayout();
            var sublinks = _layoutService.GetSublinks();
            ViewBag.Layout = layout;
            ViewBag.Sublinks = sublinks;
            var values = _context.homePages.FirstOrDefault();
            var informationFormList = _context.informationForms
                .Where(x => x.InformationFormId == informationFormId)
                .ToList();
            if (values != null)
            {
                return View(values);
            }
            else
            {
                var defaultValue = new HomePage
                { 
                    IsTaglineSectionIncluded = true,
                    TaglineSectionBgColor = "#FFFFFF",
                    Tagline = "Your Tagline Here",
                    IsPopularProductsSectionIncluded = true,
                    PopularProductsSectionTitle = "Popular Products",
                    PopularProductsSectionBgColor = "#EFEFEF",
                    PopularProduct1Title = "Product 1",
                    PopularProduct1Id = 1,
                    PopularProduct2Title = "Product 2", 
                    PopularProduct2Id = 2,
                    PopularProduct3Title = "Product 3", 
                    PopularProduct3Id = 3,
                    PopularProduct4Title = "Product 4", 
                    PopularProduct4Id = 4,
                    IsServicesSectionIncluded = true,
                    ServicesSectionTitle = "Our Services",
                    ServicesSectionBgColor = "#D8D8D8", 
                    Services1Description = "Service 1 Description", 
                    Services2Description = "Service 2 Description", 
                    Services3Description = "Service 3 Description", 
                    Services4Description = "Service 4 Description",

                    IsStatisticsSectionIncluded = true,
                    StatisticSectionBgColor = "#CCCCCC",
                    Statistic1Number = 100,
                    Statistic1Title = "Statistic 1",
                    Statistic2Number = 500,
                    Statistic2Title = "Statistic 2",
                    Statistic3Number = 1000,
                    Statistic3Title = "Statistic 3",
                    Statistic4Number = 5000,
                    Statistic4Title = "Statistic 4",

                };
                return View(defaultValue);
            }
        }
        [HttpPost]
        public IActionResult Index(InformationForm informationForm)
        {
            if (ModelState.IsValid)
            {
                // ContactForm içinde ContactFormId olduğunu varsayalım
                _context.informationForms.Add(informationForm);
                _context.SaveChanges();

                // Formun içindeki iletişim formunun ID'si olduğunu varsayıyorum
                return RedirectToAction("Index", new { informationFormId = informationForm.InformationFormId });
            }

            return View(informationForm);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}