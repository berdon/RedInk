using Engine.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Controllers
{
    public class PluginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() {
            if (User?.Identity?.IsAuthenticated != true
                && (bool) RouteData.DataTokens["RequireAuthenticated"]) {
                return RedirectToAction("Login", "Account");
            }

            var viewComponent = RouteData.DataTokens["ViewComponent"] as string;

            return View(new PluginViewModel {
                ViewComponent = viewComponent,
                Arguments = RouteData.Values
            });
        }
    }
}