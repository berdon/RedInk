using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using BlogPlugin.Models;
using BlogPlugin.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BlogPlugin.Controllers
{
    public class PostIndexViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync([FromServices] IDbConnection connection, string postName)
        {
            connection.Open();
            var post = await connection.QueryFirstOrDefaultAsync<Post>(
                "select * from plugins_blogplugin_posts where is_published = true and title = @postName",
            new { postName });

            if (post is default)
            {
                return Content("Not found");
            }

            return View(
                "/Plugin/BlogPlugin/Views/Post.cshtml",
                new PostViewModel
                {
                    Post = post
                });
        }
    }
}