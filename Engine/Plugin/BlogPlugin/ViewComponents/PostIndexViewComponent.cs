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

namespace BlogPlugin.Controllers
{
    public class PostIndexViewComponent : ViewComponent
    {
        private readonly IDbConnection _connection;

        public PostIndexViewComponent(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IViewComponentResult> InvokeAsync(string postName)
        {
            _connection.Open();
            var post = await _connection.QueryFirstOrDefaultAsync<Post>(
                "select * from plugins_blogplugin_posts where is_published = true and title = @postName",
            new { postName });

            if (post is default)
            {
                return Content("Not found");
            }

            return View(
                "/Plugin/BlogPlugin/Views/Post/Index.cshtml",
                new PostViewModel
                {
                    Post = post
                });
        }
    }
}