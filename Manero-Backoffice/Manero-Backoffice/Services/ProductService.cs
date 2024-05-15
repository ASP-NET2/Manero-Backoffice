using Manero_Backoffice.Models;

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
}
