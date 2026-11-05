using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.hero = _context.HeroSections.FirstOrDefault();
            ViewBag.about = _context.Abouts.FirstOrDefault();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateHero(HeroSection heroSection)
        {
            _context.HeroSections.Update(heroSection);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Ana sayfa bilgileri başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Hakkımda bilgileri başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
    }
}
