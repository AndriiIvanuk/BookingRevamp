using BookingRevamp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingRevamp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<PartnerApplication> PartnerApplications { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<PendingPayment> PendingPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PropertyAmenity>()
                .HasKey(x => new
                {
                    x.PropertyId,
                    x.AmenityId
                });

            builder.Entity<PropertyAmenity>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Amenities)
                .HasForeignKey(x => x.PropertyId);

            builder.Entity<PropertyAmenity>()
                .HasOne(x => x.Amenity)
                .WithMany(x => x.PropertyAmenities)
                .HasForeignKey(x => x.AmenityId);

            builder.Entity<Property>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId);

            builder.Entity<Booking>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.PropertyId);

            builder.Entity<Booking>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "Швидкісний WI-FI",
                    Category = "Основні",
                    Icon = "wifi.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Кондиціонер",
                    Category = "Основні",
                    Icon = "air-conditioner.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 3,
                    Name = "Опалення",
                    Category = "Основні",
                    Icon = "heating.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 4,
                    Name = "Телевізор",
                    Category = "Основні",
                    Icon = "tv.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 5,
                    Name = "Генератор",
                    Category = "Основні",
                    Icon = "generator.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 6,
                    Name = "Ліфт",
                    Category = "Основні",
                    Icon = "elevator.png",
                    Size = "s",
                },
                new Amenity
                {
                    Id = 7,
                    Name = "Шафа або гардероб",
                    Category = "Спальня",
                    Icon = "wardrobe.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 8,
                    Name = "Постільна білизна",
                    Category = "Спальня",
                    Icon = "bedclothes.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 9,
                    Name = "Праска",
                    Category = "Спальня",
                    Icon = "iron.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 10,
                    Name = "Сейф",
                    Category = "Спальня",
                    Icon = "safe.png",
                    Size = "m",
                },
                 new Amenity
                 {
                     Id = 11,
                     Name = "Повністю обладнана кухня",
                     Category = "Кухня та харчування",
                     Icon = "kitchen-set.png",
                     Size = "l",
                 },
                new Amenity
                {
                    Id = 12,
                    Name = "Холодильник",
                    Category = "Кухня та харчування",
                    Icon = "freezer.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 13,
                    Name = "Плита",
                    Category = "Кухня та харчування",
                    Icon = "stove.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 14,
                    Name = "Духова піч",
                    Category = "Кухня та харчування",
                    Icon = "oven.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 15,
                    Name = "Кавоварка",
                    Category = "Кухня та харчування",
                    Icon = "coffee-maker.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 16,
                    Name = "Електрочайник",
                    Category = "Кухня та харчування",
                    Icon = "kettle.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 17,
                    Name = "Мікрохвильова піч",
                    Category = "Кухня та харчування",
                    Icon = "microwave.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 18,
                    Name = "Власна ванна кімната",
                    Category = "Ванна кімната та прання",
                    Icon = "separate-bath.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 19,
                    Name = "Душ",
                    Category = "Ванна кімната та прання",
                    Icon = "shower.png",
                    Size = "s",
                },
                new Amenity
                {
                    Id = 20,
                    Name = "Ванна",
                    Category = "Ванна кімната та прання",
                    Icon = "bathtub.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 21,
                    Name = "Пральна машина",
                    Category = "Ванна кімната та прання",
                    Icon = "washing-machine.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 22,
                    Name = "Рушники",
                    Category = "Ванна кімната та прання",
                    Icon = "towels.png",
                    Size = "m",
                },
                 new Amenity
                 {
                     Id = 23,
                     Name = "Косметичні засоби",
                     Category = "Ванна кімната та прання",
                     Icon = "cosmetics.png",
                     Size = "l",
                 },
                new Amenity
                {
                    Id = 24,
                    Name = "Балкон",
                    Category = "Територія та вигляд",
                    Icon = "balcony.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 25,
                    Name = "Тераса",
                    Category = "Територія та вигляд",
                    Icon = "terrace.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 26,
                    Name = "Вид на море",
                    Category = "Територія та вигляд",
                    Icon = "sea-view.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 27,
                    Name = "Вид на гори",
                    Category = "Територія та вигляд",
                    Icon = "mountain-view.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 28,
                    Name = "Закрита тереторія",
                    Category = "Територія та вигляд",
                    Icon = "closed-area.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 29,
                    Name = "Сад",
                    Category = "Територія та вигляд",
                    Icon = "garden.png",
                    Size = "s",
                },
                new Amenity
                {
                    Id = 30,
                    Name = "Дитяче ліжечко",
                    Category = "Для дітей",
                    Icon = "baby-bed.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 31,
                    Name = "Ігрова зона",
                    Category = "Для дітей",
                    Icon = "play-area.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 32,
                    Name = "Прибирання",
                    Category = "Додаткові можливості",
                    Icon = "cleaning.png",
                    Size = "m",
                },
                new Amenity
                {
                    Id = 33,
                    Name = "Сніданок включено",
                    Category = "Додаткові можливості",
                    Icon = "breakfast.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 34,
                    Name = "Відеоспостереження",
                    Category = "Безпека",
                    Icon = "security-camera.png",
                    Size = "l",
                },
                new Amenity
                {
                    Id = 35,
                    Name = "Укриття",
                    Category = "Безпека",
                    Icon = "bunker.png",
                    Size = "m",
                }
            );
        }
    }
}