using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Manero_Backoffice.Tests.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly ProductService _productService;
    private readonly Mock<IConfiguration> _configurationMock;

    public ProductServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _configurationMock = new Mock<IConfiguration>();

        // Mock configuration setup
        SetupConfigurationMocks();

        _productService = new ProductService(_httpClient, _configurationMock.Object);
    }

    private void SetupConfigurationMocks()
    {
        var sections = new Dictionary<string, string>
        {
            { "ApiStrings:GetAllProducts", "http://fakeapi/products" },
            { "ApiStrings:GetAllProductsByFilter", "http://fakeapi/products/filter" },
            { "ApiStrings:GetProductByFilter", "http://fakeapi/product" },
            { "ApiStrings:CreateProduct", "http://fakeapi/products/create" },
            { "ApiStrings:UpdateProduct", "http://fakeapi/products/update" },
            { "ApiStrings:DeleteProductBase", "http://fakeapi/products/delete" },
            { "ApiStrings:DeleteProductCode", "fakeCode" }
        };

        foreach (var section in sections)
        {
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns(section.Value);
            _configurationMock.Setup(x => x.GetSection(section.Key)).Returns(mockSection.Object);
        }
    }

    [Fact]
    public async Task GetProducts_ValidRequest_ReturnsProducts()
    {
        // Arrange
        var products = new List<ProductModel>
        {
            new ProductModel { Id = "product1", Title = "Product1" },
            new ProductModel { Id = "product2", Title = "Product2" }
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _productService.GetProducts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Count);
        Assert.Equal(products[0].Id, result[0].Id);
        Assert.Equal(products[1].Id, result[1].Id);
    }

    [Fact]
    public async Task GetProductsByFilter_ValidRequest_ReturnsFilteredProducts()
    {
        // Arrange
        var products = new List<ProductModel>
        {
            new ProductModel { Id = "product1", Title = "Product1" },
            new ProductModel { Id = "product2", Title = "Product2" }
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _productService.GetProductsByFilter("category1", "subCategory1", "format1", true, true, true, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Count);
        Assert.Equal(products[0].Id, result[0].Id);
        Assert.Equal(products[1].Id, result[1].Id);
    }

    [Fact]
    public async Task GetProductById_ValidRequest_ReturnsProduct()
    {
        // Arrange
        var product = new UpdateProductModel { Id = "product1", Title = "Product1" };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new List<UpdateProductModel> { product }), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _productService.GetProductById("product1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }

    [Fact]
    public async Task CreateProductAsync_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var newProduct = new ProductRegistrationForm { Title = "NewProduct" };

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
        var result = await _productService.CreateProductAsync(newProduct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateProductAsync_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var updatedProduct = new UpdateProductModel { Id = "product1", Title = "UpdatedProduct" };

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
        var result = await _productService.UpdateProductAsync(updatedProduct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteProduct_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var productId = "product1";

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
        var result = await _productService.DeleteProduct(productId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetProducts_ApiThrowsException_ReturnsEmptyList()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.GetProducts();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProductsByFilter_ApiThrowsException_ReturnsEmptyList()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.GetProductsByFilter("category1", "subCategory1", "format1", true, true, true, true);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProductById_ApiThrowsException_ReturnsNull()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.GetProductById("product1");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateProductAsync_ApiThrowsException_ReturnsFalse()
    {
        // Arrange
        var newProduct = new ProductRegistrationForm { Title = "NewProduct" };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.CreateProductAsync(newProduct);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateProductAsync_ApiThrowsException_ReturnsFalse()
    {
        // Arrange
        var updatedProduct = new UpdateProductModel { Id = "product1", Title = "UpdatedProduct" };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.UpdateProductAsync(updatedProduct);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteProduct_ApiThrowsException_ReturnsFalse()
    {
        // Arrange
        var productId = "product1";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Request failed"));

        // Act
        var result = await _productService.DeleteProduct(productId);

        // Assert
        Assert.False(result);
    }
}
