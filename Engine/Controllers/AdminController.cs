using Engine.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Engine.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index([FromQuery] string url) {
            return View();
        }
    }
}