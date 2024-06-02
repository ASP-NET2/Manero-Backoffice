using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Services;

public class ProductService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<ProductModel>> GetProducts()
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:GetAllProducts");
            var result = await Http.GetFromJsonAsync<List<ProductModel>>(url);
            return result ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
    public async Task<List<ProductModel>> GetProductsByFilter(string category, string subCategory, string format, bool onSale, bool isFavorite, bool Bestseller, bool featuredProduct)
    {
        try
        {
            var query = _configuration.GetValue<string>("ApiStrings:GetAllProductsByFilter");

            if (!string.IsNullOrEmpty(category))
                query += $"&category={category}";
            if (!string.IsNullOrEmpty(subCategory))
                query += $"&subCategory={subCategory}";
            if (!string.IsNullOrEmpty(format))
                query += $"&format={format}";
            if (onSale)
                query += $"&onSale={onSale}";
            if (isFavorite)
                query += $"&isFavorite={isFavorite}";
            if (Bestseller)
                query += $"&Bestseller={Bestseller}";
            if (featuredProduct)
                query += $"&featuredProduct={featuredProduct}";

            var result = await Http.GetFromJsonAsync<List<ProductModel>>(query);
            return result ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<UpdateProductModel> GetProductById(string productId)
    {
        try
        {
            var query = _configuration.GetValue<string>("ApiStrings:GetProductByFilter");
            query += $"&id={productId}";
            // https://maneroproductsfunction.azurewebsites.net/api/SortProduct?code=9mfmPoUUxcBnyQ-CyFMAlX3U_ldlg9X-kEBpdyF3fyPLAzFuIV8aww%3D%3D?id=3d89ec1f-fd51-4279-88e8-9a425f3edc5b

            var result = await Http.GetFromJsonAsync<List<UpdateProductModel>>(query);
            if (result != null && result.Count > 0)
                return result[0];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
            return null!;
    }

    public async Task<bool> CreateProductAsync(ProductRegistrationForm product)
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:CreateProduct");
            var respons = await Http.PostAsJsonAsync(url, product);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false!;
    }

    public async Task<bool> UpdateProductAsync(UpdateProductModel product)
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:UpdateProduct");
            var respons = await Http.PutAsJsonAsync(url, product);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false!;
    }

    public async Task<bool> DeleteProduct(string Id)
    {
        try
        {
            var baseUrl = _configuration.GetValue<string>("ApiStrings:DeleteProductBase");
            var code = _configuration.GetValue<string>("ApiStrings:DeleteProductCode");
            var apiUrl = $"{baseUrl}/{Id}?code={code}";
            
            var respons = await Http.DeleteAsync(apiUrl);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false;
    }
}
