using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Layout
{
    public interface ILayoutComponentHelper
    {
         Task<IHtmlContent> ContainerAsync(IViewComponentHelper helper, string name);
         Task<IHtmlContent> ContentAsync(IViewComponentHelper helper);
    }
}