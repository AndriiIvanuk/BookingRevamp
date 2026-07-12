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

        private readonly LiqPayService _liqPayService;

        public BookingController(AppDbContext db, BookingPriceService bookingPriceService,LiqPayService liqPayService)
        {
            _db = db;

            _bookingPriceService = bookingPriceService;

            _liqPayService = liqPayService;
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
                    return Content("Не вдалося отримати UserId.");
                }

                var property = await _db.Properties
                    .Include(x => x.Images)
                    .Include(x => x.Amenities)
                        .ThenInclude(x => x.Amenity)
                    .FirstOrDefaultAsync(x => x.Id == model.PropertyId);

                if (property == null)
                {
                    return Content("Property не знайдено.");
                }

                if (!ModelState.IsValid)
                {
                    var errors = string.Join("\n",
                        ModelState
                            .Where(x => x.Value!.Errors.Any())
                            .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}"));

                    return Content("ModelState невалідний:\n\n" + errors);
                }

                model.CheckIn = DateTime.SpecifyKind(model.CheckIn, DateTimeKind.Utc);
                model.CheckOut = DateTime.SpecifyKind(model.CheckOut, DateTimeKind.Utc);

                if (model.CheckIn.Date < DateTime.Today)
                {
                    return Content("Некоректна дата заїзду.");
                }

                if (model.CheckOut <= model.CheckIn)
                {
                    return Content("Некоректна дата виїзду.");
                }

                if (model.Guests < 1 || model.Guests > property.MaxGuests)
                {
                    return Content("Некоректна кількість гостей.");
                }

                var bookingPrice = _bookingPriceService.Calculate(
                    property.PricePerNight,
                    model.CheckIn,
                    model.CheckOut);

                var orderId = Guid.NewGuid().ToString("N");

                var pendingPayment = new PendingPayment
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

                    PaymentMethod = model.PaymentMethod,

                    LiqPayOrderId = orderId
                };

                if (model.PaymentMethod == "Arrival")
                {
                    var booking = new Booking
                    {
                        PropertyId = property.Id,

                        UserId = userId,

                        CheckIn = model.CheckIn,

                        CheckOut = model.CheckOut,

                        Guests = model.Guests,

                        FullName = pendingPayment.FullName,

                        Email = pendingPayment.Email,

                        PhoneNumber = pendingPayment.PhoneNumber,

                        Amount = bookingPrice.GrandTotal,

                        Currency = property.Currency,

                        PaymentMethod = "Arrival",

                        Status = BookingStatus.PendingArrival,

                        LiqPayOrderId = orderId
                    };

                    _db.Bookings.Add(booking);

                    await _db.SaveChangesAsync();

                    booking.BookingNumber = $"WB-{booking.CreatedAt:yyyy-MM}-{booking.Id:D5}";

                    await _db.SaveChangesAsync();

                    return RedirectToAction("BookingResult", "Home", new { bookingId = booking.Id });
                }

                _db.PendingPayments.Add(pendingPayment);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Payment), new { pendingPaymentId = pendingPayment.Id });

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
        public async Task<IActionResult> Payment(int pendingPaymentId)
        {
            var pendingPayment = await _db.PendingPayments
                .FirstOrDefaultAsync(x => x.Id == pendingPaymentId);

            if (pendingPayment == null)
            {
                return NotFound();
            }

            var property = await _db.Properties
                .FirstOrDefaultAsync(x => x.Id == pendingPayment.PropertyId);

            if (property == null)
            {
                return NotFound();
            }

            var resultUrl = Url.Action(
                nameof(Success),
                "Booking",
                null,
                Request.Scheme)!;

            var serverUrl = Url.Action(
                nameof(Callback),
                "Booking",
                null,
                Request.Scheme)!;

            var booking = new Booking
            {
                PropertyId = pendingPayment.PropertyId,
                Property = property,

                UserId = pendingPayment.UserId,

                CheckIn = pendingPayment.CheckIn,
                CheckOut = pendingPayment.CheckOut,

                Guests = pendingPayment.Guests,

                FullName = pendingPayment.FullName,
                Email = pendingPayment.Email,
                PhoneNumber = pendingPayment.PhoneNumber,

                Amount = pendingPayment.Amount,
                Currency = pendingPayment.Currency,

                PaymentMethod = pendingPayment.PaymentMethod,

                LiqPayOrderId = pendingPayment.LiqPayOrderId
            };

            var payment = _liqPayService.CreatePayment(pendingPayment, resultUrl, serverUrl);

            var model = new PaymentViewModel
            {
                PendingPayment = pendingPayment,

                Property = property,

                Data = payment.Data,

                Signature = payment.Signature
            };

            return View(model);
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
