using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ContactList()
        {
            ViewBag.SocialMedias = _context.SocialMedias.ToList(); 

            var values = _context.Contacts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return View(contact);
            }
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("ContactList");
        }
        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value = _context.Contacts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return View(contact);
            }
            try
            {
                _context.Contacts.Update(contact);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "İletişim bilgileri başarıyla güncellendi.";
                return RedirectToAction("ContactList");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                return View(contact);
            }
        }
    }
}
