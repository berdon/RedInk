using System;
using System.Data;
using System.Threading.Tasks;
using Engine.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Middleware
{
    public class UserMiddleware : IMiddleware
    {
        private IUserService _userService;

        public UserMiddleware(IUserService userService) {
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.User.Identity.IsAuthenticated) {
                await next(context);
                return;
            }

            var user = await _userService.GetUserById(long.Parse(context.Request.Cookies["UserId"]));
            context.Items.Add("User", user);

            await next(context);
        }
    }
}