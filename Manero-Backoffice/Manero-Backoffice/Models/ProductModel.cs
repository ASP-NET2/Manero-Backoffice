namespace Manero_Backoffice.Models;

public class ProductModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string Language { get; set; } = "Swedish";
    public string? Pages { get; set; }
    public string? PublishDate { get; set; }
    public string? Publisher { get; set; }
    public string? ISBN { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();

    public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    public List<FormatModel> Formats { get; set; } = new List<FormatModel>();
    public List<SubCategoryModel> SubCategories { get; set; } = new List<SubCategoryModel>();
}

