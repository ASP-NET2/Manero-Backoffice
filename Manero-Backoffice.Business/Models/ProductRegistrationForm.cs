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
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public string? Publisher { get; set; }
    public string? ISBN { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool OnSale { get; set; }
    public bool BestSeller { get; set; }
    public bool IsFavorite { get; set; }
    public bool FeaturedProduct { get; set; }
    [Required]
    public string CategoryName { get; set; } = null!; 
    [Required]
    public string SubCategoryName { get; set; } = null!; 
    [Required]
    public string FormatName { get; set; } = null!; 
}