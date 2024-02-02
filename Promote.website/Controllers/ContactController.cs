using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using System.Linq;

namespace Promote.website.Controllers
{
    public class ContactController : Controller
    {
        private readonly Context _context;

        public ContactController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int contactFormId)
        {
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
