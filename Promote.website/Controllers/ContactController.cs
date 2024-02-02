using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using System.Collections.Generic;
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
        public IActionResult Index(int id)
        {
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

                // Formun içinde ilgili iletişim sayfasının ID'si olduğunu varsayıyorum
                // 'contactForm.PageId' ifadesini, gerçek sayfa ID'sini tutan özelliğinizle değiştirin
                return RedirectToAction("Index", new { id = contactForm.ContactFormId });
            }

            return View(contactForm);
        }


    }
}