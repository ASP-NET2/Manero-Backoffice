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
            var url = _configuration.GetValue<string>("ProductApis:GetAllSubCategories");
            var result = await Http.GetFromJsonAsync<List<SubCategoryModel>>(url);
            return result ?? [];
        }
        catch (System.Exception) { }
        return [];
    }
}
