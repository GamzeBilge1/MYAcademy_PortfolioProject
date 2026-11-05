using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            this._context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Abouts.FirstOrDefault();
            ViewBag.imageUrl = value.ImageUrl;
            ViewBag.fullName = value.FullName;
            ViewBag.location = value.Location;
            ViewBag.jobTitle = value.JobTitle;
           
            ViewBag.title = value.Title;
            ViewBag.description1 = value.Description1;
            ViewBag.description2 = value.Description2;

            ViewBag.statistic1Value = value.Statistic1Value;
            ViewBag.statistic1Title = value.Statistic1Title;

            ViewBag.statistic2Value = value.Statistic2Value;
            ViewBag.statistic2Title = value.Statistic2Title;

            ViewBag.statistic3Value = value.Statistic3Value;
            ViewBag.statistic3Title = value.Statistic3Title;

            ViewBag.cvUrl = value.CvUrl;
            return View();
        }

    }
}
