using System.ComponentModel.DataAnnotations;

namespace Interview.Data.Models;

public class Customer
{
    [Required]
    public string Email { get; set; }

    [Required]
    public int LastProduct { get; set; }
}
