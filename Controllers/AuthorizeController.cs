using BookingRevamp.Models.ViewModels;
using BookingRevamp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingRevamp.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly AuthorizeService _authorizeService;

        public AuthorizeController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _authorizeService.Login(
                model.Email,
                model.Password);

            if (user == null)
            {
                ViewBag.LoginError = true;

                ModelState.AddModelError("", "Неправильний email або пароль");

                return View(model);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email)
    };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _authorizeService.Register(
                model.Name,
                model.SurName,
                model.Patronymic,
                model.Email,
                model.PhoneNumber,
                model.Password);

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, model.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string? email)
        {
            var model = new ForgotPasswordViewModel
            {
                Email = email
            };

            return View(model);
        }
    }
}