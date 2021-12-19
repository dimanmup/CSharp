using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_Core_Auth_Base.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        public IActionResult ForUsers()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error401(string returnUrl)
        {
            return View((object)returnUrl);
        }
    }
}
