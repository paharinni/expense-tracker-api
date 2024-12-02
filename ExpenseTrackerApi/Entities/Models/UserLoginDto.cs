namespace ExpenseTrackerApi.Entities.Models;

public class UserLoginDto
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}