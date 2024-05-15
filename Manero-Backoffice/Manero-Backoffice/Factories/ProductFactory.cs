using Manero_Backoffice.Models;

namespace Manero_Backoffice.Factories;

public class ProductFactory
{
    public ProductModel Create(ProductEntity entity)
    {
        return new ProductModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Author = entity.Author,
            ShortDescription = entity.ShortDescription,
            LongDescription = entity.LongDescription,
            Language = entity.Language,
            Pages = entity.Pages,
            PublishDate = entity.PublishDate,
            Publisher = entity.Publisher,
            ISBN = entity.ISBN,
            ImageUrls = entity.ImageUrls ?? new List<string>(),
            Categories = entity.Category.Select(c => new CategoryModel
            {
                Id = c.Id,
                Category = c.Category,
                SubCategories = c.SubCategory.Select(sc => new SubCategoryModel
                {
                    Id = sc.Id,
                    SubCategory = sc.SubCategory
                }).ToList()
            }).ToList(),
            Formats = entity.Format.Select(f => new FormatModel
            {
                FormatType = f.FormatType,
                Price = f.Price,
                DiscountPrice = f.DiscountPrice
            }).ToList(),
            SubCategories = entity.SubCategory.Select(sc => new SubCategoryModel
            {
                Id = sc.Id,
                SubCategory = sc.SubCategory
            }).ToList()
        };
    }
}
