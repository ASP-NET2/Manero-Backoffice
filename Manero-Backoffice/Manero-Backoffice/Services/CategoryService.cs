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
            var url = _configuration.GetValue<string>("ApiStrings:GetAllCategories");
            var result = await Http.GetFromJsonAsync<List<CategoryModel>>(url);
            return result ?? [];
        }
        catch (System.Exception) { }
        return [];
    }

    public async Task<bool> DeleteCategory(string Id)
    {
        try
        {
            var baseUrl = _configuration.GetValue<string>("ApiStrings:DeleteCategoryBase");
            var code = _configuration.GetValue<string>("ApiStrings:DeleteCateogryCode");
            var apiUrl = $"{baseUrl}/{Id}?code={code}";
            
            var respons = await Http.DeleteAsync(apiUrl);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false;
    }

}
