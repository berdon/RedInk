using System;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Engine.Extensions;
using Engine.Model;
using Engine.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromServices] IDbConnection connection,
            [FromForm] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            connection.Open();
            var user = await connection.QueryFirstOrDefaultAsync<User>(
                "select * from users where email = @Email", new { loginRequest.Email }
            );

            if (user is default) {
                return BadRequest();
            }

            var passwordTokens = user.Password.Split("|");

            if (loginRequest.Password.Hash(ref passwordTokens[1]).Equals(passwordTokens[0])) {
                user.IsAuthenticated = true;
                user.AuthenticationType = "cookie";

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(user)
                );
                HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());

                return Redirect("/");
            }

            return BadRequest();
        }
    }
}