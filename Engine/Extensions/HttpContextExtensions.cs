using Engine.Model;
using Microsoft.AspNetCore.Http;

namespace Engine.Extensions
{
    public static class HttpContextExtensions
    {
        public static User User(this HttpContext context) {
            return context.Items["User"] as User;
        }
    }
}