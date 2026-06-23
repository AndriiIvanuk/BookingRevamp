using BookingRevamp.Data;
using BookingRevamp.Models;
using BookingRevamp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly GoogleStorageService _storage;

        public PartnerController(AppDbContext db, GoogleStorageService storage){
            _db = db;

            _storage = storage;
        }

        [HttpGet]
        public async Task<IActionResult> CreateProperty()
        {
            var model = new CreatePropertyViewModel
            {
                Amenities = await _db.Amenities
                    .OrderBy(x => x.Id)
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyViewModel model)
        {
            Console.WriteLine("Create property POST start");

            ModelState.Remove("Country");

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Content(string.Join("\n", errors));
            }

            Console.WriteLine("Model valid");

            Console.WriteLine($"Request.Form.Files.Count = {Request.Form.Files.Count}");

            Console.WriteLine($"Images count = {model.Images?.Count ?? 0}");

            Console.WriteLine("Before redirect");
            const int maxFiles = 8;

            const long maxFileSize = 10 * 1024 * 1024;

            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            Console.WriteLine($"Request.Form.Files.Count = {Request.Form.Files.Count}");

            if(model.Images == null || !model.Images.Any())
            {
                Console.WriteLine("NO IMAGES");

                ModelState.AddModelError(
                    "Images",
                    "Завантажте хоча б одну фотографію."
                );

                return View(model);
            }

            if(model.Images.Count > maxFiles)
            {
                Console.WriteLine("TOO MANY IMAGES");

                ModelState.AddModelError(
                    "Images",
                    $"Можна завантажити максимум {maxFiles} фотографій."
                );

                return View(model);
            }

            foreach (var file in model.Images)
            {

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();


                if (!allowedExtensions.Contains(extension))
                {
                    Console.WriteLine($"BAD EXTENSION: {extension}");

                    ModelState.AddModelError("Images", $"Файл {file.FileName} має недозволений формат.");

                    return View(model);
                }

                if (file.Length > maxFileSize)
                {
                    Console.WriteLine($"FILE TOO BIG: {file.FileName}");

                    ModelState.AddModelError("Images", $"Файл {file.FileName} перевищує 10 МБ.");

                    return View(model);
                }

            }

            Console.WriteLine("Selected amenities:");

            foreach (var id in model.SelectedAmenities)
            {
                Console.WriteLine(id);
            }

            using var transaction = await _db.Database.BeginTransactionAsync();

            Console.WriteLine("Transaction started");

            try{

                var property = new Property
                {

                    PropertyType = model.PropertyType,

                    City = model.City,

                    Address = model.Address,

                    Object = model.Object,

                    MaxGuests = model.MaxGuests,

                    AllowPets = model.AllowPets,

                    AllowChildren = model.AllowChildren,

                    Description = model.Description,

                    Rules = model.Rules,

                    PricePerNight = model.PricePerNight,

                    Currency = "₴",

                    Country = model.Country,

                };

                Console.WriteLine("Property object created");

                _db.Properties.Add(property);

                Console.WriteLine("Property added to context");

                await _db.SaveChangesAsync();

                Console.WriteLine("Property saved");

                Console.WriteLine("Property created ID: " + property.Id);

                if(model.SelectedAmenities != null)
{
                    foreach(var amenityId in model.SelectedAmenities.Distinct())
                    {
                        _db.PropertyAmenities.Add(new PropertyAmenity
                        {
                            PropertyId = property.Id,
                            AmenityId = amenityId
                        });
                    }

                    await _db.SaveChangesAsync();
                }

                foreach (var file in model.Images)
                {

                    var imageUrl = await _storage.UploadAsync(file);

                    _db.PropertyImages.Add(new PropertyImage
                    {
                        PropertyId = property.Id,

                        ImagePath = imageUrl,

                        FileName = file.FileName,
                    });

                }

                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return RedirectToAction(
                    "PropertyCreated",
                    new{
                        id = property.Id
                    });

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return Content(
                    ex.ToString()
                    + "\n\nINNER:\n"
                    + ex.InnerException?.ToString()
                );
            }
        }

        [HttpGet]
        public async Task<IActionResult> PropertyCreated(int id)
        {
            bool propertyExists = await _db.Properties.AnyAsync(p => p.Id == id);

            if(!propertyExists)
            {
                return RedirectToAction(nameof(CreateProperty));
            }

            return View();
        }
    }
}
