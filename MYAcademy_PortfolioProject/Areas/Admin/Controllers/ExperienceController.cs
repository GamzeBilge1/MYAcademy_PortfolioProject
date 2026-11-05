using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

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

        public IActionResult ExperienceList()
        {
            var values = context.Experiences.ToList();
            return View(values);
        }
    }
}
