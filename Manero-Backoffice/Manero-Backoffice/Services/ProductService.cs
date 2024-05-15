using Manero_Backoffice.Models;

namespace Manero_Backoffice.Services;

public class ProductService(HttpClient http)
{
    private readonly HttpClient Http = http;

    public async Task<List<ProductModel>> GetProducts()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<List<ProductModel>>("https://maneroproductsfunction.azurewebsites.net/api/GetAllProducts");
                
                return result ?? new List<ProductModel>();
            }
            catch
            {
                return new List<ProductModel>();
            }
        }
}
