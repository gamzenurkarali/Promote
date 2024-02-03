using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Promote.website.Models;
using Promote.website.Services;
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
        public IActionResult Index(int contactFormId)
        {
            var layout = _layoutService.GetLayout();

            var sublinks = _layoutService.GetSublinks();
            ViewBag.Layout = layout;
            ViewBag.Sublinks = sublinks;

            var contact = _context.contactPages.FirstOrDefault();
            var contactFormList = _context.contactForms
                .Where(x => x.ContactFormId == contactFormId)
                .ToList();

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
                // ContactForm içinde ContactFormId olduğunu varsayalım
                _context.contactForms.Add(contactForm);
                _context.SaveChanges();

                // Formun içindeki iletişim formunun ID'si olduğunu varsayıyorum
                return RedirectToAction("Index", new { contactFormId = contactForm.ContactFormId });
            }

            return View(contactForm);
        }
    }
}
