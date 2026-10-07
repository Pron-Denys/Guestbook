using Microsoft.AspNetCore.Mvc;

namespace Guestbook.Controllers
{
    using Guestbook.Models;
    using Microsoft.EntityFrameworkCore;
    using Guestbook.Repository;

    public class CommentsController(IRepository repository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var messages = await repository.GetMessagesToListAsync();
            return View(messages);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string? comment)
        {
            if (comment == null) return View();
            comment = comment.Trim();
            string login = HttpContext.Session.GetString("login")!;
            var user = repository.GetUserByLogin(login);
            string day = DateTime.Now.Day.ToString().Length == 1 ? $"0{DateTime.Now.Day}" : DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString().Length == 1 ? $"0{DateTime.Now.Month}" : DateTime.Now.Month.ToString();
            var message = new Message { Date = $"{day}.{month}.{DateTime.Now.Year}", Text=comment, User = user };
            await repository.AddMessageAsync(message);
            await repository.SaveChangesAsync();
            return RedirectToAction("Index", "Comments");
        }
    }
}
