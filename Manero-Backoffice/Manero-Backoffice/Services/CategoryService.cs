using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Services;

public class CategoryService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<CategoryModel>> GetCategories()
    {
        try
        {
            var url = _configuration.GetValue<string>("ProductApis:GetAllCategories");
            var result = await Http.GetFromJsonAsync<List<CategoryModel>>(url);
            return result ?? [];
        }
        catch (System.Exception) { }
        return [];
    }

}
