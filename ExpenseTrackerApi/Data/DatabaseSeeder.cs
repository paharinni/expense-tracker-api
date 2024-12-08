using System.Security.Cryptography;
using System.Text;
using ExpenseTrackerApi.Abstractions;
using ExpenseTrackerApi.Entities.Enums;
using ExpenseTrackerApi.Entities.Models;
using ExpenseTrackerApi.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext, IPasswordService passwordService)
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
                PasswordHash = passwordService.HashPassword("Test123!"),
                Role = UserRole.SuperAdmin
            };

            var admin = new User
            {
                Username = "admin",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "1234567890",
                Email = "admin@example.com",
                PasswordHash = passwordService.HashPassword("admin@example.com"),
                Role = UserRole.Admin
            };

            dbContext.Users.AddRange(superAdmin, admin);
            await dbContext.SaveChangesAsync();
        }
    }
}
