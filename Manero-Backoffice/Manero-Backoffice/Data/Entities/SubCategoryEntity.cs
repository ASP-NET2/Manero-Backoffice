namespace Manero_Backoffice.Entities;

public class SubCategoryEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PartitionKey { get; set; } = "SubCategory";
    public string? SubCategory { get; set; } 
}
