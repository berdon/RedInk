using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Engine.Extensions
{
    public static class RazorPageExtensions
    {
        public static Task<IHtmlContent> ContainerAsync(this RazorPage<object> page, string name)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var typeInfo = page.GetType().GetTypeInfo();
            var helper = (IViewComponentHelper) typeInfo.DeclaredFields.Where(f => f.FieldType == typeof(IViewComponentHelper)).First().GetValue(page);

            return helper.InvokeAsync("Container", name);
        }

        public static Task<IHtmlContent> ContentAsync(this RazorPage<object> helper)
        {
            return null;
        }
    }
}