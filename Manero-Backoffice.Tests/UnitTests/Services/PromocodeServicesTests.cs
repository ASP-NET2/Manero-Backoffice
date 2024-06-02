using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Manero_Backoffice.Tests.UnitTests.Services;

public class PromocodeServicesTests
{
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly PromocodeService _promocodeService;
        private readonly Mock<IConfiguration> _configurationMock;

        public PromocodeServicesTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _configurationMock = new Mock<IConfiguration>();

            // Mock configuration setup
            var getAllPromocodesSection = new Mock<IConfigurationSection>();
            getAllPromocodesSection.Setup(x => x.Value).Returns("http://fakeapi/promocodes");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:GetAllPromocodes")).Returns(getAllPromocodesSection.Object);

            var deletePromocodeBaseSection = new Mock<IConfigurationSection>();
            deletePromocodeBaseSection.Setup(x => x.Value).Returns("http://fakeapi/promocode");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:DeletePromocodeBase")).Returns(deletePromocodeBaseSection.Object);

            var deletePromocodeCodeSection = new Mock<IConfigurationSection>();
            deletePromocodeCodeSection.Setup(x => x.Value).Returns("fakeCode");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:DeletePromocodeCode")).Returns(deletePromocodeCodeSection.Object);

            var createPromoCodeSection = new Mock<IConfigurationSection>();
            createPromoCodeSection.Setup(x => x.Value).Returns("http://fakeapi/promocode/create");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:CreatePromoCode")).Returns(createPromoCodeSection.Object);

            var updatePromoCodeSection = new Mock<IConfigurationSection>();
            updatePromoCodeSection.Setup(x => x.Value).Returns("http://fakeapi/promocode/update");
            _configurationMock.Setup(x => x.GetSection("ApiStrings:UpdatePromoCode")).Returns(updatePromoCodeSection.Object);

            _promocodeService = new PromocodeService(_httpClient, _configurationMock.Object);
        }

        [Fact]
        public async Task GetAllPromocodesAsync_ValidRequest_ReturnsPromocodes()
        {
            // Arrange
            var promocodes = new List<PromocodeModel>
            {
                new PromocodeModel { Id = "promo1", CodeName = "DISCOUNT1", Discount = 10 },
                new PromocodeModel { Id = "promo2", CodeName = "DISCOUNT2", Discount = 20 }
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(promocodes), Encoding.UTF8, "application/json")
                });

            // Act
            var result = await _promocodeService.GetAllPromocodesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(promocodes.Count, result.Count);
            Assert.Equal(promocodes[0].Id, result[0].Id);
            Assert.Equal(promocodes[1].Id, result[1].Id);
        }

        [Fact]
        public async Task DeletePromocodeAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var promocodeId = "promo1";

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            var result = await _promocodeService.DeletePromocodeAsync(promocodeId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreatePromocodeAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var newPromocode = new PromoCodeRegistrationForm { CodeName = "NEWCODE", Discount = 15 };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created
                });

            // Act
            var result = await _promocodeService.CreatePromocodeAsync(newPromocode);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdatePromocodeAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var updatedPromocode = new PromocodeModel { Id = "promo1", CodeName = "UPDATEDCODE", Discount = 25 };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            var result = await _promocodeService.UpdatePromocodeAsync(updatedPromocode);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllPromocodesAsync_ApiThrowsException_ReturnsEmptyList()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _promocodeService.GetAllPromocodesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeletePromocodeAsync_ApiThrowsException_ReturnsFalse()
        {
            // Arrange
            var promocodeId = "promo1";

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _promocodeService.DeletePromocodeAsync(promocodeId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreatePromocodeAsync_ApiThrowsException_ReturnsFalse()
        {
            // Arrange
            var newPromocode = new PromoCodeRegistrationForm { CodeName = "NEWCODE", Discount = 15 };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _promocodeService.CreatePromocodeAsync(newPromocode);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePromocodeAsync_ApiThrowsException_ReturnsFalse()
        {
            // Arrange
            var updatedPromocode = new PromocodeModel { Id = "promo1", CodeName = "UPDATEDCODE", Discount = 25 };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _promocodeService.UpdatePromocodeAsync(updatedPromocode);

            // Assert
            Assert.False(result);
        }
}
