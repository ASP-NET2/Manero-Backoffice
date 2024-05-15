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
            var url = _configuration.GetValue<string>("ProductApis:GetAllProducts");
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
            var url = _configuration.GetValue<string>("ProductApis:CreateProduct");
            var respons = await Http.PostAsJsonAsync(url, product);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false!;
    }
}
