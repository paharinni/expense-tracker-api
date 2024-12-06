using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Entities.Models;
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

    public UserController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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
        var createdUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = user.PasswordHash,
            Username = user.Username,
        };

        _dbContext.Users.Add(createdUser);
        await _dbContext.SaveChangesAsync();

        return Ok(createdUser);
    }

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
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
}
    