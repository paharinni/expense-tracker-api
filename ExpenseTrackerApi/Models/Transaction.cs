namespace ExpenseTrackerApi.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public DateTime DateCompleted { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    
    public Category Category { get; set; }
    public User User { get; set; }
}