using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public UserController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //Get all
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        return await _dbContext.Users
            .Select(u => u)
            .ToListAsync();
    }
}