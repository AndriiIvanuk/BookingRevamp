using BookingRevamp.Data;
using BookingRevamp.Models;
using BookingRevamp.Services;
using BookingRevamp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingRevamp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDbContext _db;

        private readonly BookingPriceService _bookingPriceService;

        public BookingController(AppDbContext db,BookingPriceService bookingPriceService)
        {
            _db = db;

            _bookingPriceService = bookingPriceService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(BookingViewModel model)
        {
            try
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return Content("1. Не вдалося отримати UserId.");
                }

                var property = await _db.Properties
                    .Include(x => x.Images)
                    .Include(x => x.Amenities)
                        .ThenInclude(x => x.Amenity)
                    .FirstOrDefaultAsync(x => x.Id == model.PropertyId);

                if (property == null)
                {
                    return Content("2. Property не знайдено.");
                }

                if (!ModelState.IsValid)
                {
                    var errors = string.Join("\n",
                        ModelState
                            .Where(x => x.Value!.Errors.Any())
                            .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}"));

                    return Content("3. ModelState невалідний:\n\n" + errors);
                }

                model.CheckIn = DateTime.SpecifyKind(model.CheckIn, DateTimeKind.Utc);
                model.CheckOut = DateTime.SpecifyKind(model.CheckOut, DateTimeKind.Utc);

                if (model.CheckIn.Date < DateTime.Today)
                {
                    return Content("4. Некоректна дата заїзду.");
                }

                if (model.CheckOut <= model.CheckIn)
                {
                    return Content("5. Некоректна дата виїзду.");
                }

                if (model.Guests < 1 || model.Guests > property.MaxGuests)
                {
                    return Content("6. Некоректна кількість гостей.");
                }

                bool isBooked = await _db.Bookings.AnyAsync(x =>
                    x.PropertyId == property.Id &&
                    x.Status != BookingStatus.Cancelled &&
                    model.CheckIn < x.CheckOut &&
                    model.CheckOut > x.CheckIn);

                if (isBooked)
                {
                    return Content("7. Дати вже зайняті.");
                }

                var bookingPrice = _bookingPriceService.Calculate(
                    property.PricePerNight,
                    model.CheckIn,
                    model.CheckOut);

                var booking = new Booking
                {
                    PropertyId = property.Id,

                    UserId = userId,

                    CheckIn = model.CheckIn,

                    CheckOut = model.CheckOut,

                    Guests = model.Guests,

                    FullName = model.BookingForAnotherPerson
                        ? model.GuestFullName!
                        : model.FullName,

                    Email = model.BookingForAnotherPerson
                        ? model.GuestEmail!
                        : model.Email,

                    PhoneNumber = model.BookingForAnotherPerson
                        ? model.GuestPhoneNumber!
                        : model.PhoneNumber,

                    Amount = bookingPrice.GrandTotal,

                    Currency = property.Currency,

                    Status = model.PaymentMethod == "Arrival"
                        ? BookingStatus.PendingArrival
                        : BookingStatus.PendingPayment,

                    PaymentMethod = model.PaymentMethod,

                    CardLast4 = model.PaymentMethod == "Online"
                        ? model.CardNumber!.Replace(" ", "")[^4..]
                        : null,

                    LiqPayOrderId = Guid.NewGuid().ToString("N")
                };

                _db.Bookings.Add(booking);

                await _db.SaveChangesAsync();

                booking.BookingNumber =
                    $"WB-{booking.CreatedAt:yyyy-MM}-{booking.Id:D5}";

                await _db.SaveChangesAsync();

                if (booking.PaymentMethod == "Arrival")
                {
                    return RedirectToAction(
                        "BookingResult",
                        "Home",
                        new { bookingId = booking.Id });
                }

                return RedirectToAction(
                    nameof(Payment),
                    new { bookingId = booking.Id });
            }
            catch (Exception ex)
            {
                return Content(
                    $"Виникла помилка:\n\n" +
                    $"Тип: {ex.GetType().FullName}\n\n" +
                    $"Повідомлення:\n{ex.Message}\n\n" +
                    $"StackTrace:\n{ex.StackTrace}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int bookingId)
        {
            var booking = await _db.Bookings
                .Include(x => x.Property)
                .FirstOrDefaultAsync(x => x.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Callback()
        {
            // Тут буде обробка LiqPay Callback

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Success(int bookingId)
        {
            var booking = await _db.Bookings
                .Include(x => x.Property)
                .FirstOrDefaultAsync(x => x.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpGet]
        public IActionResult Failed()
        {
            return View();
        }
    }
}
