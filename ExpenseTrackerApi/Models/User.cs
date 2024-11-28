using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Models;

public class User
{
    [Required]
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("first_name")]
    public string FirstName { get; set; }
    [Required]
    [Column("last_name")]
    public string LastName { get; set; }
    [Required]
    [Column("username")]
    public string Username { get; set; }
    [Required]
    [Column("phone_number")]
    public int PhoneNumber { get; set; }
    [Required]
    [Column("email")]
    public string Email { get; set; }
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}