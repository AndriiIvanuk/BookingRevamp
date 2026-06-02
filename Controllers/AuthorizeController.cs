using BookingRevamp.Data;
using BookingRevamp.Models;
using BookingRevamp.Models.ViewModels;
using BookingRevamp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace BookingRevamp.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly AuthorizeService _authorizeService;

        private readonly AppDbContext _context;

        public AuthorizeController(AuthorizeService authorizeService, AppDbContext context)
        {
            _authorizeService = authorizeService;

            _context = context;
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

            var user = await _authorizeService.Login(model.Email, model.Password);

            if (user == null)
            {
                ViewBag.LoginError = true;

                ModelState.AddModelError("Password", "Неправильний email або пароль");

                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),

                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

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
            bool passwordEmpty = string.IsNullOrWhiteSpace(model.Password);

            bool confirmPasswordEmpty = string.IsNullOrWhiteSpace(model.ConfirmPassword);

            if (passwordEmpty && confirmPasswordEmpty)
            {
                ViewBag.PasswordGroupError = true;

                ModelState["ConfirmPassword"]?.Errors.Clear();

                ModelState.AddModelError("ConfirmPassword", "Ці поля не можна пропустити");
            }

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
                new Claim(ClaimTypes.Name, model.Email),

                new Claim(ClaimTypes.Role, "User")
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> BecomePartner()
        {
            var email = User.Identity.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new BecomePartnerViewModel
            {
                Name = user.Name,
                SurName = user.SurName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BecomePartner(BecomePartnerViewModel model)
        {
            var email = User.Identity.Name;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            bool cardEmpty = string.IsNullOrWhiteSpace(model.CardNumber);

            bool expiryEmpty = string.IsNullOrWhiteSpace(model.ExpiryDate);

            bool cvvEmpty = string.IsNullOrWhiteSpace(model.CVV);

            if (cardEmpty && expiryEmpty && cvvEmpty)
            {
                ViewBag.CardGroupError = true;

                ViewBag.HideCvvError = true;

                ModelState.AddModelError("", "Поля з даними рахунку не можуть бути порожніми");
            }

            else if (!cardEmpty && expiryEmpty && cvvEmpty)
            {
                ViewBag.HideCvvError = true;
            }

            if (!cardEmpty)
            {
                var digitsOnly = model.CardNumber.Replace(" ", "");

                if (digitsOnly.Length < 16)
                {
                    ModelState.AddModelError("CardNumber", "Номер картки повинен містити 16 цифр");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Name = user.Name;
                model.SurName = user.SurName;
                model.Patronymic = user.Patronymic;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;

                return View(model);
            }

            string? passportPath = null;

            if (model.PassportFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PassportFile.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "passports");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.PassportFile.CopyToAsync(stream);
                }

                passportPath = "/passports/" + fileName;
            }

            var application = new PartnerApplication
            {
                UserId = user.Id,
                
                CardNumber = model.CardNumber,
                
                ExpiryDate = model.ExpiryDate,
                
                CVV = model.CVV,
                
                PassportPath = passportPath,
                
                AcceptTerms = model.AcceptTerms
            };

            _context.PartnerApplications.Add(application);

            user.Role = "Partner";

            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),

                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Partner");
        }
    }
}