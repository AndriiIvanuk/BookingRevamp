using BookingRevamp.Data;
using BookingRevamp.Models;
using BookingRevamp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRevamp.Controllers
{
    [Authorize(Roles = "Partner")]
    public class PartnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly AppDbContext _db;

        public PartnerController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult CreateProperty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(
        CreatePropertyViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var property = new Property
            {

                Title = model.Title,

                PropertyType = model.PropertyType,

                City = model.City,

                Address = model.Address,

                MaxGuests = model.MaxGuests,

                Description = model.Description,

                Rules = model.Rules,

                PricePerNight = model.PricePerNight,

                Currency = "UAH"

            };

            _db.Properties.Add(property);

            await _db.SaveChangesAsync();

            foreach (var file in model.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine("wwwroot/uploads/properties", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _db.PropertyImages.Add(new PropertyImage
                {
                    PropertyId = property.Id, ImagePath = "/uploads/properties/" + fileName
                });
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("HomePage");
        }
    }
}
