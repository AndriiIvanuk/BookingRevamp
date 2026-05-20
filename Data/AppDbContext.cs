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
    }
}