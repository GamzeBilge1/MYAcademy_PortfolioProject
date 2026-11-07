using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
      
            private readonly PortfolioContext _context;

            public SocialMediaController(PortfolioContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult CreateSocialMedia()
            {
                return View();
            }

            [HttpPost]
            public IActionResult CreateSocialMedia(SocialMedia socialMedia)
            {
                try
                {
                    _context.SocialMedias.Add(socialMedia);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Sosyal medya hesabı başarıyla eklendi.";
                    return RedirectToAction("ContactList", "Contact");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Ekleme sırasında bir hata oluştu!";
                    return View(socialMedia);
                }
            }
        [HttpPost]
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);

                _context.SocialMedias.Remove(value);
                _context.SaveChanges();

            return RedirectToAction("ContactList", "Contact");
        }
        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            try
            {
                _context.SocialMedias.Update(socialMedia);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Sosyal medya hesabı başarıyla güncellendi.";
                return RedirectToAction("ContactList", "Contact");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                return View(socialMedia);
            }
        }
    }
    
}
