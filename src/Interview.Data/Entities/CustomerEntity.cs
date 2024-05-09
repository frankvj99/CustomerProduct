using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Data.Entities;

[Table("Customer")]
public class CustomerEntity
{
    [Key]
    public string Email { get; set; }

    [Required]
    public int LastProduct { get; set; }
}
