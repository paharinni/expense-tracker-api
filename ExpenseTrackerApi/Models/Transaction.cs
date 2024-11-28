using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Models;

public class Transaction
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("name")]
    public string Name { get; set; }
    [Required]
    [Column("amount")]
    public decimal Amount { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Required]
    [Column("date_completed")]
    public DateTime DateCompleted { get; set; }
    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }
    
    public Category Category { get; set; }
    public User User { get; set; }
}