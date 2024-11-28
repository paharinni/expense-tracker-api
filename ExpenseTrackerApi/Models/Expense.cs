namespace ExpenseTrackerApi.Models;

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
}