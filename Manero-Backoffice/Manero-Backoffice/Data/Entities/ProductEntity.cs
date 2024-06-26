﻿using Manero_Backoffice.Entities;

namespace Manero_Backoffice;

public class ProductEntity{

public string Id { get; set; } = Guid. NewGuid().ToString();
public DateTime Created { get; set; } = DateTime.Now;
public string PartitionKey { get; set; } = "Products";
public string Products { get; set; } = "Products";
public string Author { get; set; } = null!;
public string Title { get; set; } = null!;
public string? ShortDescription { get; set; }
public string? LongDescription { get; set; }
public string Language { get; set; } = "Swedish";
public string? Pages { get; set; }
public string? PublishDate { get; set; }
public string? Publisher { get; set; }
public string? ISBN { get; set; }
public List<string>? ImageUrls { get; set; }

public List<CategoryEntity> Category { get; set; } = new List<CategoryEntity>();
public List<FormatEntity> Format { get; set; } = new List<FormatEntity>();
public List<SubCategoryEntity> SubCategory { get; set; } = new List<SubCategoryEntity>();
}
