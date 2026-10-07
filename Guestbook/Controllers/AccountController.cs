using Microsoft.AspNetCore.Mvc;

namespace Guestbook.Controllers
{
    using System.Text;
    using Guestbook.Models;
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using Guestbook.Repository;
    public class AccountController(IRepository repository) : Controller
    {
        public async Task<IActionResult> CheckLogin(string login)
        {
            var users = await repository.GetUsersToListAsync();
            string value = String.Empty;
            foreach(var user in users)
            {
                if (user.Login == login)
                {
                    value = user.Login;
                    break;
                }
            }
            return Json(value != login);
        }
        public IActionResult CheckPassword(string password)
        {
            return Json(password.Length >= 4 || password.Length <= 10);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var user = repository.GetUserByLogin(model.Login);
            string name = user?.Name!;
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Неправильний логін або пароль");
                return View(model);
            }
            byte[] passwordBytes = Encoding.Unicode.GetBytes(user.Salt + model.Password);
            string hash = Convert.ToHexString(SHA256.HashData(passwordBytes));
            if (hash != user.Password)
            {
                ModelState.AddModelError(String.Empty, "Неправильний логін або пароль");
                return View(model);
            }
            HttpContext.Session.SetString("login", user.Login!);
            return RedirectToAction("Index", "Comments");
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid) return View(model);
            byte[] saldBytes = RandomNumberGenerator.GetBytes(16);
            string salt = Convert.ToHexString(saldBytes);
            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + model.Password);
            string password = Convert.ToHexString(SHA256.HashData(passwordBytes));
            User user = new User { Login = model.Login, Name = model.Name, Surname = model.Surname, Password = password, Salt = salt, Year = model.Year};
            await repository.AddUserAsync(user);
            await repository.SaveChangesAsync();
            HttpContext.Session.SetString("login", model.Login!);
            return RedirectToAction("Index", "Comments");
        }

        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Comments");
        }
    }
}
