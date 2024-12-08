namespace ExpenseTrackerApi.Entities.Models;

public class UserLoginDto
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}