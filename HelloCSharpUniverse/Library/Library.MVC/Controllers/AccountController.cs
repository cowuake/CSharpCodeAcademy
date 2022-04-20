using Library.Core.Interface;
using Library.MVC.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMainBusinessLogic _logic;


        public AccountController(IMainBusinessLogic logic)
        {
            _logic = logic;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new AccountViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (model == null)
                return View();

            var account = _logic.GetUser(model.Username);

            if(account != null)
            {
                if (account.Password.Equals(model.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, account.Role.ToString()),
                        new Claim(ClaimTypes.Name, account.Username)
                    };

                    var properties = new AuthenticationProperties
                    {
                        ExpiresUtc = System.DateTimeOffset.UtcNow.AddHours(24)
                    };

                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimIdentity),
                       properties
                    );

                    return RedirectToAction("Index", "Home");   
                }
                else
                {
                    //Password sbagliata
                    ModelState.AddModelError(nameof(model.Password), "Invalid Password");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The account does not exists");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}