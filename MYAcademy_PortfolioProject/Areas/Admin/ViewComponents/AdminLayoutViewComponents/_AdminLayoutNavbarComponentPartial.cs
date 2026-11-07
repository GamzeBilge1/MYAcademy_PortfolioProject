using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutNavbarComponentPartial: ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutNavbarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var messages = _context.Messages
       .OrderByDescending(x => x.SendDate)
       .Take(3)
       .ToList();

            return View(messages);
        }
    }
}
