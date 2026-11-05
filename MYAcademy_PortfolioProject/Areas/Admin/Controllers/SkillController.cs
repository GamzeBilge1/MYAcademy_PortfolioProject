using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Skills
         .Include(x => x.SkillItems)
         .ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            try
            {
                _context.Skills.Add(skill);
                _context.SaveChanges();
                //throw new Exception("Test hatası");

                TempData["SuccessMessage"] = "Kayıt başarılı!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kayıt sırasında bir hata oluştu!";
                return View(skill);
            }
        }
        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills
                           .Include(x => x.SkillItems)
                           .FirstOrDefault(x => x.SkillId == id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            try
            {
                var value = _context.Skills
                               .Include(x => x.SkillItems)
                               .FirstOrDefault(x => x.SkillId == skill.SkillId);
                value.Title = skill.Title;
                value.Description = skill.Description;
                value.IconUrl = skill.IconUrl;

                foreach (var item in skill.SkillItems)
                {
                    var skillItem = value.SkillItems
                        .First(x => x.SkillItemId == item.SkillItemId);

                    skillItem.SkillItemTitle = item.SkillItemTitle;
                    skillItem.Level = item.Level;
                }

                _context.SaveChanges();
                //throw new Exception("Test hatası");
                TempData["SuccessMessage"] = "Güncelleme başarılı!";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                return View(skill);
            }
        }

        [HttpPost]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills
                .Include(x => x.SkillItems)
                .FirstOrDefault(x => x.SkillId == id);

            _context.SkillItems.RemoveRange(value.SkillItems);
            _context.Skills.Remove(value);
            _context.SaveChanges();
     
            return RedirectToAction("Index");
        }
    }
}
