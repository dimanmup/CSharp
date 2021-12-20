using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_Core_Auth_BaseRoles.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        public IActionResult ForAuthenticated()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ForAdmin()
        {
            return View();
        }

        [Authorize(Roles = "User,Auditor")]
        public IActionResult ForUserAuditor()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error401(string returnUrl)
        {
            return View((object)returnUrl);
        }

        public IActionResult Error403(string returnUrl)
        {
            return View((object)returnUrl);
        }
    }
}
