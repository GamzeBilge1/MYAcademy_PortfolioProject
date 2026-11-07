using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                             .SelectMany(x => x.Errors)
                             .FirstOrDefault()?.ErrorMessage; 
                TempData["ErrorMessage"] =error ??  "Lütfen alanları doğru doldurunuz!";
                return Redirect("/Default/Index#contact");
            }
            try
            {
                message.SendDate = DateTime.Now;
                message.IsRead = false;

                _context.Messages.Add(message);    
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Mesaj gönderilirken bir hata oluştu!";
            }

            return Redirect("/Default/Index#contact");
        }
    }
}
