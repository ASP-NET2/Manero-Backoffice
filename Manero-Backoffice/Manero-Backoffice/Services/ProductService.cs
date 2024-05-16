using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Components.Pages;

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

    public async Task<bool> CreateProduct(ProductRegistrationForm product)
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
