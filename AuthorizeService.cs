using BookingRevamp.Models;
using BookingRevamp.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BookingRevamp.Services
{
    public class AuthorizeService
    {
        private readonly AppDbContext _context;

        public AuthorizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Register(
            string username,
            string surname,
            string patronymic,
            string email,
            string phoneNumber,
            string password)
        {
            var user = new User
            {
                Name = username,
                SurName = surname,
                Patronymic = patronymic,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Login(string email, string password)
        {
            var hash = HashPassword(password);

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hash);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }
    }
}
