using Manero_Backoffice.Models;

namespace Manero_Backoffice.Services;

public class ProductService(HttpClient http)
{
    private readonly HttpClient Http = http;

    public async Task<List<ProductModel>> GetProducts()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<List<ProductModel>>("https://maneroproductsfunction.azurewebsites.net/api/GetAllProducts?code=006xKHFr-cdRL_Km-7dSq_giwxeV3xxeM8EA1MqjCcZzAzFuic6r3Q==");
                return result ?? new List<ProductModel>();
            }
            catch
            {
                return new List<ProductModel>();
            }
        }
}
