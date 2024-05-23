using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Services;

public class FormatService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<FormatModel>> GetFormats()
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:GetAllFormats");
            var result = await Http.GetFromJsonAsync<List<FormatModel>>(url);
            return result ?? [];
        }
        catch (System.Exception) { }
        return [];
    }

}
