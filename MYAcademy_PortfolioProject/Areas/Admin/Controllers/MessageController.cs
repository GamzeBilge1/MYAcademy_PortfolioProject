using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext context;

        public MessageController(PortfolioContext context)
        {
            this.context = context;
        }

        public IActionResult MessageList()
        {
            var values = context.Messages.ToList();
            return View(values);
        }
        
        public IActionResult MessageDetail(int id)
        {
            var value = context.Messages.Find(id);
            return View(value);
        }
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            context.Messages.Add(message);
            context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var value = context.Messages.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateMessage(Message message)
        {
            context.Messages.Update(message);
            context.SaveChanges();
            return RedirectToAction("MessageList");

        }
        public IActionResult DeleteMessage(int id)
        {
            var value = context.Messages.Find(id);
            context.Messages.Remove(value);
            context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}
