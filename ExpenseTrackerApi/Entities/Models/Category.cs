using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Entities.Models;

public class Category
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}