using System.ComponentModel.DataAnnotations;

namespace Interview.Data.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}
