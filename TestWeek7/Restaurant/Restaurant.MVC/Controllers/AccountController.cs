using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Entities;
using Restaurant.Core.Interface;
using Restaurant.MVC.Models.Account;
using Restaurant.MVC.Utils;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IBusinessLogic _logic;


        public AccountController(IBusinessLogic logic)
        {
            _logic = logic;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new AccountViewModel();

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (model == null)
                return View();

            if (!ModelState.IsValid)
                return View(model);

            var result = _logic.CheckAccount(model.Email, model.Password);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }
            else
            {
                var account = _logic.GetAccount(model.Email);

                await LogAccount(account);

                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterAccountViewModel();

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterAccountViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Errore generating model");
                return View();
            }

            bool existing = _logic.GetAccount(model.Email) != null;

            if (existing)
            {
                ModelState.AddModelError(string.Empty, $"An account with email '{model.Email}' has already been registered");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError
                (
                    string.Empty,
                    "Passwords do not match"
                );
            }

            if (ModelState.IsValid)
            {
                Account account = model.ToUserAccount();

                var result = _logic.RegisterAccount(model.Email, model.Password);

                if (!result.Success)
                    ModelState.AddModelError(string.Empty, result.Message);
                else
                {
                    // Auto-login
                    await LogAccount(account);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task LogAccount(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, account.Role.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Name, account.Email)
            };

            var properties = new AuthenticationProperties
            {
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddHours(1)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync
            (
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimIdentity),
               properties
            );
        }

        [AllowAnonymous]
        public IActionResult CheckUsernameAvailability(string username)
        {
            var account = _logic.GetAccount(username);

            if (account == null)
                return Json(true);

            return Json(false);
        }
    }
}