using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Engine.Layout
{
    public class LayoutComponentHelper : ILayoutComponentHelper
    {
        private readonly IViewComponentSelector _viewComponentSelector;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<MvcOptions> _optionsAccessor;

        public LayoutComponentHelper(
            IViewComponentSelector viewComponentSelector,
            IHttpContextAccessor httpContextAccessor,
            IActionContextAccessor actionContextAccessor,
            IServiceProvider serviceProvider,
            IOptions<MvcOptions> optionsAccessor)
        {
            _viewComponentSelector = viewComponentSelector;
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
            _serviceProvider = serviceProvider;
            _optionsAccessor = optionsAccessor;
        }
        public Task<IHtmlContent> ContainerAsync(IViewComponentHelper helper, string name)
        {
            return helper.InvokeAsync("Container", name);
        }

        public async Task<IHtmlContent> ContentAsync(IViewComponentHelper helper)
        {
            var actionContext = _actionContextAccessor.ActionContext;
            var viewComponentName = actionContext.RouteData.DataTokens["ViewComponent"] as string;

            var compositeValueProvider = await CompositeValueProvider.CreateAsync(actionContext, _optionsAccessor.Value.ValueProviderFactories);
            var pluginViewComponent = _viewComponentSelector.SelectComponent(viewComponentName);
            var parameterBinder = ActivatorUtilities.CreateInstance<ParameterBinder>(_serviceProvider);

            var parameterBag = new Dictionary<string, object>();

            foreach (var parameter in pluginViewComponent.Parameters)
            {
                var parameterDescriptor = new ParameterDescriptor {
                    BindingInfo = BindingInfo.GetBindingInfo(parameter.GetCustomAttributes()),
                    Name = parameter.Name,
                    ParameterType = parameter.ParameterType,
                };

                var result = await parameterBinder.BindModelAsync(
                    actionContext,
                    compositeValueProvider,
                    parameterDescriptor);
                
                parameterBag[parameter.Name] = result.IsModelSet ? result.Model : null;
            }
            return await helper.InvokeAsync(viewComponentName, parameterBag);
        }
    }
}