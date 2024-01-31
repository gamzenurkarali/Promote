using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class ProductPagesController : Controller
    {
        private readonly Context _context;

        public ProductPagesController(Context context)
        {
            _context = context;
        }

        public IActionResult ProductListPage()
        {
            var page = _context.productLists.FirstOrDefault();  
            var productList=_context.products.ToList();
             
            var values = new ProductListPageViewModel {
                productListPage = page,
                products=productList
            };
            return View(values);
             
        }
        public IActionResult ProductDetailPage(int id)
        {
            var page = _context.productDetailPages.FirstOrDefault();
            var product = _context.products.FirstOrDefault(x => x.Id == id);

            var values = new ProductDetailPageViewModel
            {
                detailPage = page,
                product = product
            };

            if (values != null)
            {
                return View(values);
            }
            else
            {
                var defaultValue = new ProductDetailPageViewModel
                {
                    detailPage = new ProductDetailPage
                    {
                        Tab1Title = "Dahil olan hizmetler",
                        Tab2Title = "Dahil olmayan hizmetler",
                        Tab3Title = "İptal politikası",
                        DetailedDescriptionTitle = "Detaylı Bilgiler",
                        
                    },
                    product = new Product
                    {
                        ProductName = "Rize",
                        ProductImageFileName = "a.png",
                        Fee = 0,
                        Description = "sss",
                        Tab1Description = "aaa",
                        Tab2Description = "aaa",
                        Tab3Description = "aaa",
                        DetailedDescriptionBgImage = "a.png",
                        DetailedDescription = "akak",
                        
                        
                    }
                };

                return View(defaultValue);
            }
        }

    }
}
