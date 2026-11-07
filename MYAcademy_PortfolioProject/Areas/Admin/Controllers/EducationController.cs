using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Educations.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return View(education);
            }

            try
            {
                _context.Educations.Add(education);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Eğitim başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kayıt sırasında bir hata oluştu!";
                return View(education);
            }
        }

        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return View(education);
            }
            try
            {
                _context.Educations.Update(education);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Eğitim başarıyla güncellendi";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                return View(education);
            }
        }

        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            _context.Educations.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
