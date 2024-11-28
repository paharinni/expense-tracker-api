namespace ExpenseTrackerApi.Models;

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Sum { get; set; }
    public string Description { get; set; }
}