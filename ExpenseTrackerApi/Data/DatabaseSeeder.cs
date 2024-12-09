using ExpenseTrackerApi.Abstractions;
using ExpenseTrackerApi.Entities.Enums;
using ExpenseTrackerApi.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;

    public DatabaseSeeder(ApplicationDbContext context, IPasswordService passwordService, IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _configuration = configuration;
    }
    
    public async Task SeedAsync()
    {
        if (!await _context.Users.AnyAsync())
        {
            var superAdminConfig = _configuration.GetSection("DefaultUsers:SuperAdmin");
            var adminConfig = _configuration.GetSection("DefaultUsers:Admin");
            
            var superAdmin = new User
            {
                Username = superAdminConfig["Username"],
                FirstName = superAdminConfig["FirstName"],
                LastName = superAdminConfig["LastName"],
                PhoneNumber = superAdminConfig["PhoneNumber"],
                Email = superAdminConfig["Email"],
                PasswordHash = _passwordService.HashPassword(superAdminConfig["Password"]),
                Role = UserRole.SuperAdmin
            };

            var admin = new User
            {
                Username = adminConfig["Username"],
                FirstName = adminConfig["FirstName"],
                LastName = adminConfig["LastName"],
                PhoneNumber = adminConfig["PhoneNumber"],
                Email = adminConfig["Email"],
                PasswordHash = _passwordService.HashPassword(adminConfig["Password"]),
                Role = UserRole.Admin
            };

            _context.Users.AddRange(superAdmin, admin);
            await _context.SaveChangesAsync();
        }
    }
}
