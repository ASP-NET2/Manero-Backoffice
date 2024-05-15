namespace Manero_Backoffice.Models;

public class ProductModel
    {
        public string Id { get; set; } = null!;
        public DateTime? Created { get; set; }
        public string PartitionKey { get; set; } = null!; 
        public string Products { get; set; } = null!; 
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string DiscountPrice { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string Pages { get; set; } = null!;
        public string PublishDate { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string ImageUrl { get; set; } = null!; 

        public string Category { get; set; } = null!; 
        public string SubCategory { get; set; } = null!; 
        public string Format { get; set; } = null!; 
    }



// {
//     public string Id { get; set; } = null!;
//     public string Title { get; set; } = null!;
//     public string Author { get; set; } = null!;
//     public string? ShortDescription { get; set; }
//     public string? LongDescription { get; set; }
//     public string Language { get; set; } = null!;
//     public string? Pages { get; set; }
//     public string? PublishDate { get; set; }
//     public string? Publisher { get; set; }
//     public string? ISBN { get; set; }
//     public List<string> ImageUrls { get; set; } = new List<string>();

//     public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
//     public List<FormatModel> Formats { get; set; } = new List<FormatModel>();
//     public List<SubCategoryModel> SubCategories { get; set; } = new List<SubCategoryModel>();
// }

