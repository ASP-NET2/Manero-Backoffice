namespace Manero_Backoffice.Entities;

public class CategoryEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PartitionKey { get; set; } = "Category";
    public string? Category { get; set; } 
    public List<SubCategoryEntity> SubCategory { get; set; } = new List<SubCategoryEntity>();
}
