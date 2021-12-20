using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Web_Core_Auth_BaseRoles.Models;

namespace Web_Core_Auth_BaseRoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Users
                .Include(u => u.Roles)
                .Select(u => new UserModel 
                {
                    Email = u.Email,
                    Roles = u.Roles.Select(r => r.Name).ToList()
                }));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
