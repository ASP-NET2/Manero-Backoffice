using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Services;

public class SubCategoryService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<SubCategoryModel>> GetSubCategories()
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:GetAllSubCategories");
            var result = await Http.GetFromJsonAsync<List<SubCategoryModel>>(url);
            return result ?? [];
        }
        catch (System.Exception) { }
        return [];
    }

    public async Task<bool> DeleteSubCategory(string Id)
    {
        try
        {
            var baseUrl = _configuration.GetValue<string>("ApiStrings:DeleteSubCategoryBase");
            var code = _configuration.GetValue<string>("ApiStrings:DeleteSubCateogryCode");
            var apiUrl = $"{baseUrl}/{Id}?code={code}";
            
            var respons = await Http.DeleteAsync(apiUrl);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (System.Exception) { }
        return false;
    }
}
