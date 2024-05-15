namespace Manero_Backoffice.Models;

public class CategoryModel
{
    public string Id { get; set; } = null!;
    public string? Category { get; set; }
    public List<SubCategoryModel> SubCategories { get; set; } = new List<SubCategoryModel>();
}

