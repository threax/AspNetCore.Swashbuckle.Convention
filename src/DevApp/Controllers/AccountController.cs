using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Threax.AspNetCore.IdServerAuth;

namespace DevApp.Controllers
{
    [Authorize(AuthenticationSchemes = AuthCoreSchemes.Cookies)]
    public class AccountController : Controller
    {
        public IActionResult Relogin()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutOfIdServer();
            return View();
        }
    }
}
