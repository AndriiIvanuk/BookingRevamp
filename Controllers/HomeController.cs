using BookingRevamp.Data;
using BookingRevamp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingRevamp.Controllers;

public class HomeController : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Houses = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Будинок")
                .Take(4)
                .ToListAsync(),

            Hotels = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Готель" && x.Country == "Амстердам")
                .Take(4)
                .ToListAsync(),

            Apartments = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Апартаменти" && x.Country == "Рим")
                .Take(4)
                .ToListAsync(),

            Villas = await _db.Properties
                .Include(x => x.Images)
                .Where(x => x.PropertyType == "Вілла" && x.Country == "Барселона")
                .Take(4)
                .ToListAsync()
        };

        return View(model);
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
    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Messages()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
