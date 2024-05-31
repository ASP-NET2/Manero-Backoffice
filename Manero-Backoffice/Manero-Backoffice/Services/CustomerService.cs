using Manero_Backoffice.Business.Models;
using Newtonsoft.Json;

namespace Manero_Backoffice.Services;

public class CustomerService(HttpClient http, ILogger<CustomerService> logger, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly ILogger<CustomerService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly string _baseUrl = "https://userproviderwebapp.azurewebsites.net/api/User/account_users"; // Replace with your actual API endpoint

    public async Task<List<CustomerModel>> GetCustomersAsync()
    {
        try
        {
            var response = await _http.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(content);

                if (customers != null)
                {
                    _logger.LogInformation("Customers retrieved.");
                    return customers;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : CustomerService.GetCustomersAsync() :: {ex.Message}");
        }

        _logger.LogWarning("Failed to get customers.");
        return null!;
    }

    public async Task<List<CustomerModel>> GetAllCustomersAsync()
    {
        try
        {
            var url = _configuration.GetValue<string>("ApiStrings:GetAllCustomers");
            var result = await _http.GetFromJsonAsync<List<CustomerModel>>(url);
            return result ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : CustomerService.GetAllCustomersAsync() :: {ex.Message}");
        }
        _logger.LogWarning("Failed to get customers.");
        return [];
    }
}
