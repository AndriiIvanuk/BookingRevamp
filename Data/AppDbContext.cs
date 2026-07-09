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
                }
            );
        }
    }
}