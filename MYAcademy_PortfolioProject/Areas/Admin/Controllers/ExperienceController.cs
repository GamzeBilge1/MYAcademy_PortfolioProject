using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext context;

        public ExperienceController(PortfolioContext context)
        {
            this.context = context;
        }

        public IActionResult ExperienceList(int page=1)
        {
            int pageSize = 4;

            var values = context.Experiences
                .OrderBy(x => x.ExperienceId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(
                context.Experiences.Count() / (double)pageSize);
            ViewBag.TotalCount = context.Experiences.Count();
            return View(values);
        }

        [HttpPost]
        public IActionResult BulkAction(List<int> ids, string actionType)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Lütfen en az bir deneyim seçiniz.");
            }
            var values = context.Experiences.Where(x => ids.Contains(x.ExperienceId)).ToList();
            if (actionType == "activate")
            {
                foreach (var item in values)
                {
                    item.isActive = true;
                }
            }
            else if (actionType == "deactivate")
            {
                foreach (var item in values)
                {
                    item.isActive = false;
                }
            }
            else if (actionType == "delete")
            {
                context.Experiences.RemoveRange(values);
            }
            context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return RedirectToAction("ExperienceList");
            }
            try
            {
                context.Experiences.Add(experience);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Deneyim başarıyla eklendi!";
                return RedirectToAction("ExperienceList");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kayıt sırasında bir hata oluştu!";
                return RedirectToAction("ExperienceList");
            }
        }
        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = context.Experiences.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen bilgileri kontrol ediniz.";
                return View(experience);
            }
            try
            {
                context.Experiences.Update(experience);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Deneyim başarıyla güncellendi!";
                return RedirectToAction("ExperienceList");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                return View(experience);
            }
        }
        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            var value = context.Experiences.Find(id);
            context.Experiences.Remove(value);
            context.SaveChanges();

            return RedirectToAction("ExperienceList");
        }
    }
}
