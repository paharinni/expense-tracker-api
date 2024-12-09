using ExpenseTrackerApi.Abstractions;
using ExpenseTrackerApi.Entities.Enums;
using ExpenseTrackerApi.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public DatabaseSeeder(ApplicationDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }
    
    public async Task SeedAsync()
    {
        if (!await _context.Users.AnyAsync())
        {
            var superAdmin = new User
            {
                Username = "superadmin",
                FirstName = "superadmin",
                LastName = "superadmin",
                PhoneNumber = "1234567890",
                Email = "superadmin@example.com",
                PasswordHash = _passwordService.HashPassword("superadmin"),
                Role = UserRole.SuperAdmin
            };

            var admin = new User
            {
                Username = "admin",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "1234567890",
                Email = "admin@example.com",
                PasswordHash = _passwordService.HashPassword("admin"),
                Role = UserRole.Admin
            };

            _context.Users.AddRange(superAdmin, admin);
            await _context.SaveChangesAsync();
        }
    }
}
