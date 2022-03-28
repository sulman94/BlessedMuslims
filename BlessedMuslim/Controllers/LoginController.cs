using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlessedMuslim.Models;
using Microsoft.EntityFrameworkCore;

namespace BlessedMuslim.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var context = new BlessedMuslim_DBContext();
            var user = await context.Users.Include(x=>x.Role).Where(s => s.Name == email && s.Password == password).FirstOrDefaultAsync();
            //var role = await context.Role.Select(s => s.Users.Where(x=>x.Id == user.Id).FirstOrDefault()).FirstOrDefaultAsync();

            if (user!= null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, email));
                identity.AddClaim(new Claim(ClaimTypes.Name, email));
                identity.AddClaim(new Claim(ClaimTypes.UserData, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.RoleName));

                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

                if (user.RoleId == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Username or Password is incorrect Please try again!";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}