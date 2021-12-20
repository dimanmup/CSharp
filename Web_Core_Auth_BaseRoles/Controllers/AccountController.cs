using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web_Core_Auth_BaseRoles.Models;

namespace Web_Core_Auth_BaseRoles.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext context;

        public AccountController(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Создает куки аутентифицированности с именем и ролями.
        /// </summary>
        private async Task Authenticate(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            foreach (Role role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            ClaimsIdentity ci = new ClaimsIdentity(claims, "ApplicationCookie");

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(ci));
        }

        #region Sign in/out

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            // if (!ModelState.IsValid) ...

            User user = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(
                    u => u.Email == model.Email
                    && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Неверная почта или пароль!";

                return View(model);
            }

            await Authenticate(user);

            return RedirectToAction("ForAuthenticated", "Test");
        }

        public async Task<IActionResult> SignOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #endregion Sign in/out

        /// <summary>
        /// Создает связь много-много.
        /// </summary>
        public IActionResult CreateRelationships()
        {
            foreach (User user in context.Users.Include(u => u.Roles))
            {
                if (user.Roles.Any())
                {
                    continue;
                }

                var newRoles = context.Roles
                    .AsEnumerable()
                    .Where(newRole => !user.Roles.Exists(oldRole => oldRole.Id == newRole.Id)
                        && user.Email.Contains(newRole.Name));

                if (newRoles.Any())
                {
                    user.Roles.AddRange(newRoles);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
