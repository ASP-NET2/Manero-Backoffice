using Manero_Backoffice.Business.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Manero_Backoffice.Services;

public class EmployeeService(HttpClient http, ILogger<EmployeeService> logger)
{
    private readonly HttpClient _http = http;
    private readonly ILogger<EmployeeService> _logger = logger;
    private readonly string _baseUrl = "https://employeeprovider.azurewebsites.net/api/Employees";
    //private readonly string _baseUrl = "https://localhost:7299/api/Employees";

    public async Task<RequestResult> CreateEmployeeAsync(EmployeeRegistrationModel employeeModel )
    {

        try
        {
            var response = await _http.PostAsJsonAsync(_baseUrl, employeeModel);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Employee created.");
                return new RequestResult { Succeeded = true };
            }
            else
            {
                _logger.LogWarning("Employee creation failed.");
                return new RequestResult { Succeeded = false, ErrorMessage = "Employee creation failed." };
            }
          
        }

        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeService.CreateEmployee() :: {ex.Message}");
        }

        return new RequestResult { Succeeded = false, ErrorMessage = "Something went wrong. Try again later" };

    }

    public async Task<EmployeeModel> GetEmployeeAsync(string id)
    {
        try
        {
            var response = await _http.GetAsync($"{_baseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<EmployeeModel>(content);

                if(employee != null)
                {
                    _logger.LogInformation("Employee retrieved.");
                    return employee;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeService.GetEmployeeAsync() :: {ex.Message}");
        }

        _logger.LogWarning("Failed to get employee.");
        return null!;
    }

    public async Task<List<EmployeeModel>> GetEmployeesAsync()
    {
        try
        {
            var response = await _http.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(content);

                if (employees != null)
                {
                    _logger.LogInformation("Employees retrieved.");
                    return employees;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeService.GetEmployeesAsync() :: {ex.Message}");
        }

        _logger.LogWarning("Failed to get employees.");
        return null!;
    }

    public async Task<RequestResult> DeleteEmployeeAsync(string id)
    {
        try
        {
            var response = await _http.DeleteAsync($"{_baseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Employee deleted.");
                return new RequestResult { Succeeded = true };
            }
            else
            {
                _logger.LogWarning("Employee deletion failed.");
                return new RequestResult { Succeeded = false, ErrorMessage = "Failed to delete employee. Please try again." };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeService.DeleteEmployeeAsync() :: {ex.Message}");
            return new RequestResult { Succeeded = false, ErrorMessage = "Something went wrong. Try again later" };
        }
    }

    public async Task<RequestResult> UpdateEmployeeAsync(string id, EmployeeUpdateModel employeeUpdateModel)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"{_baseUrl}/{id}", employeeUpdateModel);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Employee updated.");
                return new RequestResult { Succeeded = true };
            }
            else
            {
                _logger.LogWarning("Employee update failed.");
                return new RequestResult { Succeeded = false, ErrorMessage = "Employee update failed." };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmployeeService.UpdateEmployeeAsync() :: {ex.Message}");
            return new RequestResult { Succeeded = false, ErrorMessage = "Something went wrong. Try again later" };
        }
    }


}
