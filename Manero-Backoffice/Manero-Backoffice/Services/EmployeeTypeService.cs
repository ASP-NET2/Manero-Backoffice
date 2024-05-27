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
        var response = await _httpClient.GetAsync(_baseUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var employeeTypes = JsonConvert.DeserializeObject<List<EmployeeTypeModel>>(content);
            return employeeTypes!;
        }

        return null!;
    }
}
