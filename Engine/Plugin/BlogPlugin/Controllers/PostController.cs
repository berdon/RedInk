using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using BlogPlugin.Models;
using BlogPlugin.ViewModels;

namespace BlogPlugin.Controllers
{
    public class PostController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IDbConnection connection, string postName)
        {
            connection.Open();
            var post = await connection.QueryFirstOrDefaultAsync<Post>(
                "select * from plugins_blogplugin_posts where is_published = true and title = @postName",
                new { postName });
            
            if (post is default) {
                return StatusCode(404);
            }

            return View(
                "/Plugin/BlogPlugin/Views/Post/Index.cshtml",
                new PostViewModel {
                    Post = post
                });
        }
    }
}