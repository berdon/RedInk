using Microsoft.AspNetCore.Mvc;

namespace BlogPlugin.Controllers
{
    public class IndexController : Controller
    {
        public string Index() {
            return "Index";
        }
    }
}