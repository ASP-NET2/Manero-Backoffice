using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;


namespace Manero_Backoffice.Tests.UnitTests.Services;

public class CategoryServiceTests
{
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly CategoryService _categoryService;
        private readonly Mock<IConfiguration> _configurationMock;

        public CategoryServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _configurationMock = new Mock<IConfiguration>();

            _categoryService = new CategoryService(_httpClient, _configurationMock.Object);
        }

        private void SetupConfiguration(string key, string value)
        {
            _configurationMock.Setup(c => c[key]).Returns(value);
        }

        [Fact]
        public async Task GetCategories_HandlesException_ReturnsEmptyList()
        {
            // Arrange
            var url = "https://example.com/getallcategories";
            SetupConfiguration("ApiStrings:GetAllCategories", url);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _categoryService.GetCategories();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteCategory_HandlesException_ReturnsFalse()
        {
            // Arrange
            var baseUrl = "https://example.com/deletecategory";
            var code = "123";
            var categoryId = "1";
            var apiUrl = $"{baseUrl}/{categoryId}?code={code}";

            SetupConfiguration("ApiStrings:DeleteCategoryBase", baseUrl);
            SetupConfiguration("ApiStrings:DeleteCategoryCode", code);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(apiUrl)),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _categoryService.DeleteCategory(categoryId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddCategory_HandlesException_ReturnsFalse()
        {
            // Arrange
            var category = new CategoryRegistrationForm { CategoryName = "New Category" };
            var url = "https://example.com/createcategory";

            SetupConfiguration("ApiStrings:CreateCategory", url);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _categoryService.AddCategory(category);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateCategory_HandlesException_ReturnsFalse()
        {
            // Arrange
            var category = new CategoryModel { Id = "1", CategoryName = "Updated Category" };
            var url = "https://example.com/updatecategory";

            SetupConfiguration("ApiStrings:UpdateCategory", url);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _categoryService.UpdateCategory(category);

            // Assert
            Assert.False(result);
        }
}
