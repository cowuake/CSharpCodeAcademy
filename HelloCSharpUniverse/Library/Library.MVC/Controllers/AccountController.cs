using Library.Core.Entities;
using Library.Core.Interface;
using Library.MVC.Helpers;
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

            if (!ModelState.IsValid)
                return View(model);

            var result = _logic.CheckAccount(model.Username, model.Password);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }
            else
            {
                var account = _logic.GetAccount(model.Username);

                await LogAccount(account);

                return RedirectToAction("Index", "Home");
            }

            //if(account != null)
            //{
            //    if (account.Password.Equals(model.Password))
            //    {
            //        await LogAccount(account);

            //        return RedirectToAction("Index", "Home");   
            //    }
            //    else
            //    {
            //        //Password sbagliata
            //        ModelState.AddModelError(nameof(model.Password), "Invalid Password");
            //        return View(model);
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "The account does not exists");
            //    return View(model);
            //}
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

            bool existing = _logic.GetAccount(model.Username) != null;

            if (existing)
            {
                ModelState.AddModelError(string.Empty, $"An account for username '{model.Username}' is already there");
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

                var result = _logic.RegisterAccount(model.Username, model.Password);

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
            return RedirectToAction("Index","Home");
        }

        private async Task LogAccount(Account account)
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