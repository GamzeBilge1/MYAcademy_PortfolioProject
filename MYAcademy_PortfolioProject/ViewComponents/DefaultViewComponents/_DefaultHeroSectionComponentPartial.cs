using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial :ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            this._context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.HeroSections.Take(1).FirstOrDefault();
            ViewBag.greeting = value.Greeting;
            ViewBag.title = value.Title;
            ViewBag.description = value.Description;
            ViewBag.mainImageUrl = value.MainImageUrl;
            return View();

        }
    }
}
