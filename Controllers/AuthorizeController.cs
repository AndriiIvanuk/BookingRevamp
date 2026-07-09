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
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _authorizeService.Login(model.Email, model.Password);

            if(user == null)
            {
                ViewBag.LoginError = true;

                ModelState.AddModelError("Password", "Неправильний email або пароль");

                return View(model);
            }

            await SignInUser(user);

            if(!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl)
        {
            bool surnameEmpty = string.IsNullOrWhiteSpace(model.SurName);

            bool nameEmpty = string.IsNullOrWhiteSpace(model.Name);

            if(surnameEmpty && nameEmpty)
            {
                ViewBag.NameGroupError = true;

                ModelState.AddModelError("", "Як ми можемо до тебе звертатися?");
            }

            bool passwordEmpty = string.IsNullOrWhiteSpace(model.Password);

            bool confirmPasswordEmpty = string.IsNullOrWhiteSpace(model.ConfirmPassword);

            if(passwordEmpty && confirmPasswordEmpty)
            {
                ViewBag.PasswordGroupError = true;

                ModelState["ConfirmPassword"]?.Errors.Clear();

                ModelState.AddModelError("ConfirmPassword", "Ці поля не можна пропустити");
            }

            if(!ModelState.IsValid)
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

            var user = await _authorizeService.Login(model.Email, model.Password);

            await SignInUser(user);

            if(!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

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
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user == null)
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
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login");
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
                
                
                PassportPath = passportPath,
                
                AcceptTerms = model.AcceptTerms
            };

            _context.PartnerApplications.Add(application);

            user.Role = "Partner";

            await _context.SaveChangesAsync();

            await SignInUser(user);

            return RedirectToAction("Index", "Partner");
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
                new Claim(ClaimTypes.Name, user.Name),
                
                new Claim(ClaimTypes.Surname, user.SurName),
                
                new Claim(ClaimTypes.Email, user.Email),
                
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
    }
}