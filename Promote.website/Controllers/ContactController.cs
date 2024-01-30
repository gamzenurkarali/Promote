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
            var contact = _context.contactPages.FirstOrDefault(x => x.ContactPageId == id);
            var contactForms = _context.contactForms.Where(x => x.ContactFormId == id).ToList();

            ContactViewModel model = new ContactViewModel
            {
                Contact = contact,
                ContactForm = contactForms
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
                return RedirectToAction("Index", new { id =  1});
            }

            return View(contactForm);
        }
    }
}
