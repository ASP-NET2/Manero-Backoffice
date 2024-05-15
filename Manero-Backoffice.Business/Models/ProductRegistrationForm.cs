using System.ComponentModel.DataAnnotations;

namespace Manero_Backoffice.Business.Models;

public class ProductRegistrationForm
{
    [Required]
    public string Author { get; set; } = null!;
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Price { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string? Language { get; set; }
    public string? Pages { get; set; }
    public string? PublishDate { get; set; }
    public string? Publisher { get; set; }
    public string? ISBN { get; set; }
    [Required]
    public string ImageUrl { get; set; } = null!;
    [Required]
    public string Category { get; set; } = null!;
    [Required]
    public string SubCategory { get; set; } = null!;
    public string Format { get; set; } = null!;

}
