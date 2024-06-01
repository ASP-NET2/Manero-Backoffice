using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Manero_Backoffice.Tests.UnitTests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly EmployeeService _employeeService;
    private readonly Mock<ILogger<EmployeeService>> _loggerMock;

    public EmployeeServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _loggerMock = new Mock<ILogger<EmployeeService>>();
        _employeeService = new EmployeeService(_httpClient, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateEmployeeAsync_ValidEmployee_ReturnsSuccess()
    {
        // Arrange
        var employeeModel = new EmployeeRegistrationModel
        {
            Id = "testId",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890",
            EmployeeTypeId = "testTypeId",
            AddressId = "testAddressId"
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        // Act
        var result = await _employeeService.CreateEmployeeAsync(employeeModel);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task GetEmployeeAsync_ValidId_ReturnsEmployee()
    {
        // Arrange
        var id = "testId";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new EmployeeModel { Id = id }), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _employeeService.GetEmployeeAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ValidEmployee_ReturnsSuccess()
    {
        // Arrange
        var id = "testId";
        var employeeModel = new EmployeeUpdateModel
        {
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            PhoneNumber = "1234567890",
            EmployeeTypeId = "updatedTypeId",
            AddressId = "updatedAddressId"
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        // Act
        var result = await _employeeService.UpdateEmployeeAsync(id, employeeModel);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ValidId_ReturnsSuccess()
    {
        // Arrange
        var id = "testId";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(id);

        // Assert
        Assert.True(result.Succeeded);
    }






}
