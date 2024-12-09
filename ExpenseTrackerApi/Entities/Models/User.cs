using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ExpenseTrackerApi.Entities.Enums;

namespace ExpenseTrackerApi.Entities.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    [Required]
    [Column("first_name")]
    public string? FirstName { get; set; }
    [Required]
    [Column("last_name")]
    public string? LastName { get; set; }
    [Required]
    [Column("username")]
    public string? Username { get; set; }
    [Required]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }
    [Required]
    [Column("email")]
    public string? Email { get; set; }
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    
    [Required]
    [Column("role")]
    public UserRole Role { get; set; } = UserRole.User;
    
    [JsonIgnore]
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
