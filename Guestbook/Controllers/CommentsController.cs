using Microsoft.AspNetCore.Mvc;

namespace Guestbook.Controllers
{
    using Guestbook.Models;
    using Microsoft.EntityFrameworkCore;

    public class CommentsController(GuestbookContext _db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var messages = await _db.Messages.Include(m => m.User).AsNoTracking().ToListAsync();
            return View(messages);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string? comment)
        {
            if (comment == null) return View();
            comment = comment.Trim();
            string login = HttpContext.Session.GetString("login")!;
            var user = _db.Users.FirstOrDefault(u => u.Login == login);
            string day = DateTime.Now.Day.ToString().Length == 1 ? $"0{DateTime.Now.Day}" : DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString().Length == 1 ? $"0{DateTime.Now.Month}" : DateTime.Now.Month.ToString();
            var message = new Message { Date = $"{day}.{month}.{DateTime.Now.Year}", Text=comment, User = user };
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Comments");
        }
    }
}
