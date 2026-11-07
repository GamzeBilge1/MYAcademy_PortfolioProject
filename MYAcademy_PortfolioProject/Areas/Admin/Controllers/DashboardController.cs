using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Areas.Admin.ViewModels;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(x => x.isActive),
                TotalSkills = _context.Skills.Count(),
                TotalExperiences = _context.Experiences.Count(),
                TotalEducations = _context.Educations.Count(),
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(x => !x.IsRead),
                ReadMessages = _context.Messages.Count(x => x.IsRead),

                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),
              
                CategorySummaries = _context.Projects
                    .GroupBy(x => new { x.CategoryId, x.Category.CategoryName })
                    .Select(x => new
                    {
                        CategoryName = x.Key.CategoryName,
                        ProjectCount = x.Count()
                    })
                    .ToDictionary(x => x.CategoryName, x => x.ProjectCount),

                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.ProjectId)
                    .Take(3)
                    .ToList()
            };

            return View(model);
        }
    }
}
