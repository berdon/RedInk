using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)Content("<p>Header</p>"));
        }
    }
}