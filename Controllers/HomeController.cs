using BookingRevamp.Data;
using BookingRevamp.Models;
using BookingRevamp.Services;
using BookingRevamp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BookingRevamp.Controllers;

public class HomeController : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        List<int> favoritePropertyIds = new();

        if (User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out int userId))
            {
                favoritePropertyIds = await _db.Favorites
                    .Where(x => x.UserId == userId)
                    .Select(x => x.PropertyId)
                    .ToListAsync();
            }
        }

        var model = new HomeViewModel
        {
            FavoritePropertyIds = favoritePropertyIds,

            Houses = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Будинок" && x.Country == "Франція")
                .OrderByDescending(x => x.CreatedAt)
                .Take(4)
                .ToListAsync(),

            Hotels = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Готель" && x.Country == "Амстердам")
                .OrderByDescending(x => x.CreatedAt)
                .Take(4)
                .ToListAsync(),

            Apartments = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Апартаменти" && x.Country == "Рим")
                .OrderByDescending(x => x.CreatedAt)
                .Take(4)
                .ToListAsync(),

            Villas = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Вілла" && x.Country == "Барселона")
                .OrderByDescending(x => x.CreatedAt)
                .Take(4)
                .ToListAsync()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult BookingLoginRequired(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Property(int id)
    {

        var property = await _db.Properties
            .Include(x => x.Images)
            .Include(x => x.Amenities)
                .ThenInclude(x => x.Amenity)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (property == null)
        {
            return NotFound();
        }

        return View(property);
    }

    private readonly AppDbContext _db;

    private readonly BookingPriceService _bookingPriceService;

    public HomeController(AppDbContext db, BookingPriceService bookingPriceService)
    {
        _db = db;

        _bookingPriceService = bookingPriceService;
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [Authorize]
    public IActionResult Messages()
    {
        return View();
    }

    public IActionResult Terms()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(!int.TryParse(userIdClaim, out int userId))
        {
            return RedirectToAction("Login", "Authorize");
        }

        var favoriteProperties = await _db.Favorites
            .Where(x => x.UserId == userId)
            .Include(x => x.Property)
                .ThenInclude(x => x.Images)
            .Select(x => x.Property)
            .ToListAsync();

        var model = new FavoritesViewModel
        {
            Properties = favoriteProperties,

            FavoritePropertyIds = favoriteProperties
                .Select(x => x.Id)
                .ToHashSet()
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ToggleFavorite(int propertyId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized();
        }

        var favorite = await _db.Favorites.FirstOrDefaultAsync(x =>
            x.UserId == userId &&
            x.PropertyId == propertyId);

        if (favorite == null)
        {
            _db.Favorites.Add(new Favorite
            {
                UserId = userId,
                PropertyId = propertyId
            });

            await _db.SaveChangesAsync();

            return Json(new
            {
                isFavorite = true
            });
        }

        _db.Favorites.Remove(favorite);

        await _db.SaveChangesAsync();

        return Json(new
        {
            isFavorite = false
        });
    }

    [HttpGet]
    public async Task<IActionResult> Booking(int propertyId, DateTime checkIn, DateTime checkOut, int guests)
    {
        if(!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction(nameof(BookingLoginRequired), new
            {
                returnUrl = Url.Action(nameof(Booking), "Home", new
                {
                    propertyId,
                    checkIn,
                    checkOut,
                    guests
                })
            });
        }
        var property = await _db.Properties
            .Include(x => x.Images)
            .Include(x => x.Amenities)
                .ThenInclude(x => x.Amenity)
            .FirstOrDefaultAsync(x => x.Id == propertyId);

        if(property == null)
        {
            return NotFound();
        }

        if(guests < 1 || guests > property.MaxGuests)
        {
            return BadRequest();
        }

        if(checkOut <= checkIn)
        {
            return BadRequest();
        }

        if(checkIn.Date < DateTime.Today)
        {
            return BadRequest();
        }

        var price = _bookingPriceService.Calculate(property.PricePerNight, checkIn, checkOut);

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out int userId))
        {
            return RedirectToAction("Login", "Authorize");
        }

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Authorize");
        }

        var model = new BookingViewModel
        {
            Property = property,

            PropertyId = property.Id,

            CheckIn = checkIn,

            CheckOut = checkOut,

            Guests = guests,

            Nights = price.Nights,

            TotalPrice = price.TotalPrice,

            Taxes = price.Taxes,

            GrandTotal = price.GrandTotal,

            Email = user?.Email ?? "",

            PhoneNumber = user?.PhoneNumber ?? "",

            FullName = string.Join(" ", new[]{
                user?.SurName,
                user?.Name,
                user?.Patronymic
            }.Where(x => !string.IsNullOrWhiteSpace(x)))
        };

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> BookingResult(int bookingId)
    {
        var booking = await _db.Bookings
            .Include(x => x.Property)
            .FirstOrDefaultAsync(x => x.Id == bookingId);

        if (booking == null)
        {
            return NotFound();
        }

        var model = new BookingResultViewModel
        {
            Booking = booking,

            Success = booking.Status == BookingStatus.Paid ||
                      booking.Status == BookingStatus.PendingArrival,

            Title = booking.Status == BookingStatus.Paid ||
                    booking.Status == BookingStatus.PendingArrival
                ? "Бронювання підтверджено"
                : "Оплату не завершено",

            Message = booking.Status == BookingStatus.Paid ||
                      booking.Status == BookingStatus.PendingArrival
                ? "Ваше бронювання успішно створено."
                : "На жаль, оплату не вдалося завершити."
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
