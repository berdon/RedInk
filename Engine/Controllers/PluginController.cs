using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Engine.Layout;
using Engine.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Controllers
{
    public class PluginController : Controller
    {
        private readonly ILayoutManager _layoutManager;
        private readonly IViewComponentSelector _viewComponentSelector;

        public PluginController(
            ILayoutManager layoutManager,
            IViewComponentSelector viewComponentSelector
        )
        {
            this._layoutManager = layoutManager;
            this._viewComponentSelector = viewComponentSelector;
        }

        [AllowAnonymous]
        public IActionResult Index([FromServices] IServiceProvider serviceProvider, [FromServices] IActionContextAccessor actionContextAccessor)
        {
            if (User?.Identity?.IsAuthenticated != true
                && (bool)RouteData.DataTokens["RequireAuthenticated"])
            {
                return RedirectToAction("Login", "Account");
            }

            var layout = _layoutManager.LayoutForRoute(RouteData.DataTokens["RouteName"] as string);
            var pluginViewComponentName = RouteData.DataTokens["ViewComponent"] as string;

            return View(new ViewComponentViewModel
            {
                ViewComponent = "PluginLayout",
                Arguments = new object[] {
                    _layoutManager.PathForLayoutView(layout),
                    new ViewComponentViewModel
                    {
                        ViewComponent = pluginViewComponentName,
                        Arguments = null
                    }
                }
            });
        }
    }

    public class PluginLayoutViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(object[] arguments)
        {
            var layout = arguments[0] as string;
            var viewModel = arguments[1] as ViewComponentViewModel;
            return Task.FromResult((IViewComponentResult) View(layout, viewModel));
        }
    }
}