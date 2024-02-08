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
            var documents = _layoutService.GetDocuments();
            ViewBag.relDocuments = documents;
            var sublinks = _layoutService.GetSublinks();
            ViewBag.Layout = layout;
            ViewBag.Sublinks = sublinks;

            var contact = _context.contactPages.FirstOrDefault();
            var contactFormList = _context.contactForms
                .Where(x => x.ContactFormId == contactFormId)
                .ToList();

            ContactViewModel values;

            if (contact != null || contactFormList.Count > 0)
            {
                values = new ContactViewModel
                {
                    Contact = contact ?? GetDefaultContactPage(),
                    ContactForm = contactFormList
                };
            }
            else
            {
                values = new ContactViewModel
                {
                    Contact = contact ?? GetDefaultContactPage()
                };
            }

            return View(values);
        }

        private ContactPage GetDefaultContactPage()
        {
            return new ContactPage
            { 
                ContactInfoTitle = "defaultContactInfoTitle",
                ContactInfoDescription = "defaultContactInfoDescription",
                PhoneNumber = "defaultPhoneNumber",
                EmailAddress = "defaultEmailAddress"
            };
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
