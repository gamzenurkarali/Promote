using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using Promote.website.Services;
using System.Collections.Generic;
using System.Linq;

namespace Promote.website.Controllers
{
    public class ContactController : Controller
    {
        private readonly Context _context;
        private readonly LayoutService _layoutService;
        public ContactController(Context context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var layout = _layoutService.GetLayout();
            var sublinks = _layoutService.GetSublinks();
            ViewBag.Layout = layout;
            ViewBag.Sublinks = sublinks;
            var contact = _context.contactPages.FirstOrDefault();
            var contactFormList = _context.contactForms.Where(x => x.ContactFormId == id).ToList();

            ContactViewModel model = new ContactViewModel
            {
                Contact = contact,
                ContactForm = contactFormList
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _context.contactForms.Add(contactForm);
                _context.SaveChanges();

                // Burada, eklenen formun ait olduğu sayfanın ID'sini almalısınız.
                // Eğer bu bilgiyi alamıyorsanız, sabit bir ID kullanmak yerine dinamik bir şekilde belirlemelisiniz.
                return RedirectToAction("Index", new { id = 1 });
            }

            return View(contactForm);
        }

    }
}
