using System.Security.Cryptography;
using System.Text;
using ExpenseTrackerApi.Entities.Enums;
using ExpenseTrackerApi.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data;

public static class DatabaseSeeder
{
    private static readonly ApplicationDbContext _context;
    
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var superAdmin = new User
            {
                Username = "paharinni",
                FirstName = "Serhii",
                LastName = "Pakharenko",
                PhoneNumber = "1234567890",
                Email = "test123@gmail.com",
                PasswordHash = HashPassword("Test123!"),
                Role = UserRole.SuperAdmin
            };

            var admin = new User
            {
                Username = "admin",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "1234567890",
                Email = "admin@example.com",
                PasswordHash = HashPassword("admin@example.com"),
                Role = UserRole.Admin
            };

            dbContext.Users.AddRange(superAdmin, admin);
            await dbContext.SaveChangesAsync();
        }
    }
    
    // todo
    // move hashpassword to own class

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
