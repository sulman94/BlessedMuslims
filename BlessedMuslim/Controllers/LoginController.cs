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
            var user = await context.Users.Include(x => x.Role).Where(s => s.Name == email && s.Password == password).FirstOrDefaultAsync();
            //var role = await context.Role.Select(s => s.Users.Where(x=>x.Id == user.Id).FirstOrDefault()).FirstOrDefaultAsync();

            if (user != null)
            {
                if (user.LastLoginDate != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.UserData, email));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.RoleName));

                    var principal = new ClaimsPrincipal(identity);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

                    user.LastLoginDate = DateTime.Now;
                    context.Update(user);
                    await context.SaveChangesAsync();

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
                    return View("ChangePassword");
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string Email, string oldpassword, string newpassword, string Confirmpassword)
        {
            var context = new BlessedMuslim_DBContext();
            var user = await context.Users.Where(s => s.Name == Email).FirstOrDefaultAsync();
            if (user != null)
            {
                if (!user.Password.Equals(oldpassword))
                {
                    ViewBag.ErrorMessage = "Old Password is Not Correct.";
                    return View();
                }
                else if (user.Password.Equals(newpassword))
                {
                    ViewBag.ErrorMessage = "New Password cannot be same as old password.";
                    return View();
                }
                else if (!newpassword.Equals(Confirmpassword))
                {
                    ViewBag.ErrorMessage = "New and Confirm New Password is not same.";
                    return View();
                }
                else
                {
                    user.Password = newpassword;
                    user.LastLoginDate = DateTime.Now;
                    context.Update(user);
                    await context.SaveChangesAsync();
                }
            }
            return View("Index");
        }
    }
}