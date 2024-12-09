using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseTrackerApi.Entities.Models;

public class Transaction
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("amount")]
    public decimal Amount { get; set; }
    [Column("description")]
    public required string Description { get; set; }
    [Column("date_completed")]
    public DateTime DateCompleted { get; set; }
    
    [Column("category_id")]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}