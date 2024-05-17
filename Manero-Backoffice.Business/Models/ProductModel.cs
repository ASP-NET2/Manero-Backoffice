namespace Manero_Backoffice.Business.Models; 

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
    