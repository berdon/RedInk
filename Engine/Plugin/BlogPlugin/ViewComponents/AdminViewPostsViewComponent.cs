using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlugin.ViewComponents
{
    public class AdminViewPostsViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult<IViewComponentResult>(null);
        }
    }
}