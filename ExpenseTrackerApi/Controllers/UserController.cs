using ExpenseTrackerApi.Abstractions;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Entities.Enums;
using ExpenseTrackerApi.Entities.Models;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Controllers;

[Authorize(Roles = "SuperAdmin")]
[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordService _passwordService;

    public UserController(ApplicationDbContext dbContext, IPasswordService passwordService)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == id);

        if(user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUserAsync(User user)
    {
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

        _dbContext.Users.Add(createdUser);
        await _dbContext.SaveChangesAsync();

        return Ok(createdUser);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<User>> UpdateUserAsync(Guid id, User user)
    {
        var updatedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (updatedUser == null)
        {
            return NotFound();
        }
        
        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.Email = user.Email;
        updatedUser.PhoneNumber = user.PhoneNumber;
        updatedUser.PasswordHash = user.PasswordHash;
        updatedUser.Username = user.Username;
        
        await _dbContext.SaveChangesAsync();
        
        return Ok(updatedUser);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<User>> DeleteUserAsync(Guid id)
    {
        var deletedUser = await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (deletedUser == null)
        {
            return NotFound();
        }
        
        _dbContext.Users.Remove(deletedUser);
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpPatch("{id:guid}/change-role")]
    public async Task<ActionResult<User>> UpdateUserRoleAsync(Guid id, [FromBody] UserRole newRole)
    {
        if (!Enum.IsDefined(typeof(UserRole), newRole))
        {
            return BadRequest("Invalid role.");
        }
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound($"User with required guid {id} not found.");
        }
        
        user.Role = newRole;
        await _dbContext.SaveChangesAsync();
        
        return Ok(user);
    }
}
    