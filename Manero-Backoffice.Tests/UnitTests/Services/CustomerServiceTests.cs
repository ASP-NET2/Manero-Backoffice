using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Configuration;
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
    private readonly Mock<IConfiguration> _configurationMock;

        public CustomerServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _loggerMock = new Mock<ILogger<CustomerService>>();
            _configurationMock = new Mock<IConfiguration>();

            // Mock configuration setup
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns("http://fakeapi/customers");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:GetAllCustomers")).Returns(mockSection.Object);

            _customerService = new CustomerService(_httpClient, _loggerMock.Object, _configurationMock.Object);
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

    [Fact]
        public async Task GetAllCustomersAsync_ValidRequest_ReturnsAllCustomers()
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
            var result = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customers.Count, result.Count);
            Assert.Equal(customers[0].IdentityUserId, result[0].IdentityUserId);
            Assert.Equal(customers[1].IdentityUserId, result[1].IdentityUserId);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ApiThrowsException_ReturnsEmptyListAndLogsError()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("CustomerService.GetAllCustomersAsync()")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get customers")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
}
