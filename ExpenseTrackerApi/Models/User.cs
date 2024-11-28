namespace ExpenseTrackerApi.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}