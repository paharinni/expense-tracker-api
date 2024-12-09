using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTrackerApi.Abstractions;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTrackerApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordService _passwordService;

    public AuthController(ApplicationDbContext context, IConfiguration configuration, IPasswordService passwordService)
    {
        _context = context;
        _configuration = configuration;
        _passwordService = passwordService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return BadRequest("User with this email already exists.");
        
        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            return BadRequest("Username is already taken.");
        
        if (await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber))
            return BadRequest("Phone number is already taken.");
        
        var hashedPassword = _passwordService.HashPassword(user.PasswordHash);
        
        var createdUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = hashedPassword,
            Username = user.Username,
        };
        
        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username);

        if (userFromDb == null)
        {
            return BadRequest("No such user exists.");
        }
        
        var verified = _passwordService.VerifyPassword(userFromDb.PasswordHash, userLoginDto.PasswordHash);

        if (!verified)
        {
            return BadRequest("Invalid password.");
        }
        
        var token = GenerateJwtToken(userFromDb);
        
        return Ok(new {token});
    }
    
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
