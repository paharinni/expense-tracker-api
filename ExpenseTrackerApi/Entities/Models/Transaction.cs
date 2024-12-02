using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Entities.Models;

public class Transaction
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("amount")]
    public decimal Amount { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("date_completed")]
    public DateTime DateCompleted { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
}