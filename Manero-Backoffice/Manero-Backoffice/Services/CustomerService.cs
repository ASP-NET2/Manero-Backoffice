using Manero_Backoffice.Business.Models;
using Newtonsoft.Json;

namespace Manero_Backoffice.Services;

public class CustomerService
{
    private readonly HttpClient _http;
    private readonly ILogger<CustomerService> _logger;
    private readonly string _baseUrl = "https://userproviderwebapp.azurewebsites.net/api/User/account_users"; // Replace with your actual API endpoint

    public CustomerService(HttpClient http, ILogger<CustomerService> logger)
    {
        _http = http;
        _logger = logger;
    }

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
}
