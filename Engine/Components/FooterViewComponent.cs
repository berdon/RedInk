using System.Threading.Tasks;
using Engine.Layout;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult((IViewComponentResult) Content("Footer"));
        }
    }
}