using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Services;

public class PromocodeService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient Http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<PromocodeModel>> GetAllPromocodesAsync()
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:GetAllPromocodes");
            var result = await Http.GetFromJsonAsync<List<PromocodeModel>>(url);
            return result ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<bool> DeletePromocodeAsync(string Id)
    {
        try
        {
            var baseUrl = _configuration.GetValue<string>("ApiStrings:DeletePromocodeBase");
            var code = _configuration.GetValue<string>("ApiStrings:DeletePromocodeCode");
            var apiUrl = $"{baseUrl}/{Id}?code={code}";
            
            var respons = await Http.DeleteAsync(apiUrl);
            if (respons.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public async Task<bool> CreatePromocodeAsync(PromoCodeRegistrationForm promocode)
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:CreatePromoCode");
            var response = await Http.PostAsJsonAsync(url, promocode);
            if (response.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public async Task<bool> UpdatePromocodeAsync(PromocodeModel promocode)
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:UpdatePromoCode");
            var response = await Http.PutAsJsonAsync(url, promocode);
            if (response.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

}
