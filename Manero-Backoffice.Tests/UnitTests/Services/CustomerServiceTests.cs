using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Manero_Backoffice.Tests.UnitTests.Services;

public class CustomerServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly CustomerService _customerService;
    private readonly Mock<ILogger<CustomerService>> _loggerMock;

    public CustomerServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _loggerMock = new Mock<ILogger<CustomerService>>();
        _customerService = new CustomerService(_httpClient, _loggerMock.Object);
    }

    [Fact]
    public async Task GetCustomersAsync_ValidRequest_ReturnsCustomers()
    {
        // Arrange
        var customers = new List<CustomerModel>
        {
            new CustomerModel { IdentityUserId = "testId1", FirstName = "Test1", LastName = "User1", Email = "test1@example.com" },
            new CustomerModel { IdentityUserId = "testId2", FirstName = "Test2", LastName = "User2", Email = "test2@example.com" }
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(customers), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _customerService.GetCustomersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customers.Count, result.Count);
        Assert.Equal(customers[0].IdentityUserId, result[0].IdentityUserId);
        Assert.Equal(customers[1].IdentityUserId, result[1].IdentityUserId);
    }
}
