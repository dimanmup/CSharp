using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Web_Core_Auth_Base.Models;

namespace Web_Core_Auth_Base.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext context;

        public AccountController(AppDbContext context)
        {
            this.context = context;
        }

        #region Sign up

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserModel model)
        {
            //if (!ModelState.IsValid) ...

            User user;

            //user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            //if (user != null) ...

            user = new User()
            {
                Email = model.Email,
                Password = model.Password
            };

            context.Add(user);

            // Записать нового пользователя в БД.
            await context.SaveChangesAsync();
            model.Id = user.Id;
            Debug.WriteLine($"Created user id: {model.Id}");

            // Создать куки аутентифицированности.
            await Authenticate(model.Email);

            ViewBag.Success = "Успех!";

            return View(model);
        }

        /// <summary>
        /// Создает куки аутентифицированности.
        /// </summary>
        private async Task Authenticate(string login)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            
            ClaimsIdentity ci = new ClaimsIdentity(
                claims, 
                "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(ci));

            Debug.WriteLine($"Created user authenticated: {ci.IsAuthenticated}");
        }

        #endregion

        #region Sign in

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            // if (!ModelState.IsValid) ...

            User user = await context.Users.FirstOrDefaultAsync(
                    u => u.Email == model.Email 
                    && u.Password == model.Password);
            
            if (user == null)
            {
                ViewBag.Error = "Неверная почта или пароль!";

                return View(model);
            }

            await Authenticate(model.Email);
            
            return RedirectToAction("ForUsers", "Test");
        }

        #endregion

        public async Task<IActionResult> SignOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
