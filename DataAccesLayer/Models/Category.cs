using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.Models;

public class Category : BaseEntity
{
    [Required, MinLength(2), MaxLength(500)]
    public string? Name { get; set; }
    public string ImageUrl { get; set; } = "https://cdn-icons-png.flaticon.com/512/3843/3843517.png";

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
