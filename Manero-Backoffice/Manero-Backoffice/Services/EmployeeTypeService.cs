using System.Net.Http;
using System.Threading.Tasks;
using Manero_Backoffice.Business.Models;
using Newtonsoft.Json;

namespace Manero_Backoffice.Services;

public class EmployeeTypeService
{
    private readonly ILogger<EmployeeTypeService> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://employeeprovider.azurewebsites.net/api/EmployeeTypes";
    //private readonly string _baseUrl = "https://localhost:7299/api/EmployeeTypes";

    public EmployeeTypeService(ILogger<EmployeeTypeService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<List<EmployeeTypeModel>> GetEmployeeTypesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employeeTypes = JsonConvert.DeserializeObject<List<EmployeeTypeModel>>(content);
                return employeeTypes!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeTypeService.GetEmployeeTypesAsync() :: {ex.Message}");
        }

        _logger.LogWarning("Failed to get employee types.");
        return null!;
    }


    public async Task<string> GetEmployeeTypeIdAsync(string type)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{type}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employeeType = JsonConvert.DeserializeObject<EmployeeTypeModel>(content);

                if(employeeType != null)
                {
                    _logger.LogInformation("Employee type retrieved.");
                    return employeeType.Id;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeTypeService.GetEmployeeTypeAsync() :: {ex.Message}");
        }

        _logger.LogWarning("Failed to get employee type.");
        return null!;
    }


}
