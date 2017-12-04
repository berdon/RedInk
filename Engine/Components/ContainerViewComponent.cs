using System.Linq;
using System.Threading.Tasks;
using Engine.Layout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Engine.Components
{
    public class ContainerViewComponent : ViewComponent
    {
        private readonly ILayoutManager _layoutManager;
        private readonly DefaultViewComponentHelper _componentHelper;

        public ContainerViewComponent(ILayoutManager layoutManager, IViewComponentHelper componentHelper)
        {
            this._layoutManager = layoutManager;
            this._componentHelper = componentHelper as DefaultViewComponentHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name) {
            _componentHelper.Contextualize(ViewContext);
            
            var components = _layoutManager.ComponentsForName(name);

            var content = await Task.WhenAll(components.Select(async c => await _componentHelper.InvokeAsync(c, null)));

            return new HtmlContentsViewComponentResult(content);
        }
    }
}