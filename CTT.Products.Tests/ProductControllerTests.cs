using CTT.Products.API.Controllers;
using CTT.Products.API.DTOs.Requests;
using CTT.Products.Business.ProductService.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Domain;

namespace CTT.Products.API.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(_mockProductService.Object);
    }

    [Fact]
    public async Task RegisterProduct_ValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange  
        var productRequest = new RegisterProductRequest
        {
            Description = "Test Product",
            Price = 100.0m,
            Stock = 10,
            Categories = new List<string> { "Category1", "Category2" }
        };

        var createdProduct = new Product
        {
            Id = Guid.NewGuid(),
            Description = productRequest.Description,
            Price = productRequest.Price,
            Stock = productRequest.Stock,
            Categories = productRequest.Categories
        };

        _mockProductService
            .Setup(service => service.RegisterProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(createdProduct);

        // Act  
        var result = await _controller.RegisterProduct(productRequest);

        // Assert  
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ProductController.GetProductById), createdAtActionResult.ActionName);
        Assert.Equal(createdProduct, createdAtActionResult.Value);
    }

    [Fact]
    public async Task RegisterProduct_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange  
        RegisterProductRequest productRequest = null;

        // Act  
        var result = await _controller.RegisterProduct(productRequest);

        // Assert  
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid product data.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetProductById_ProductExists_ReturnsOk()
    {
        // Arrange  
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Description = "Test Product",
            Price = 100.0m,
            Stock = 10,
            Categories = new List<string> { "Category1", "Category2" }
        };

        _mockProductService
            .Setup(service => service.GetProductByIdAsync(productId))
            .ReturnsAsync(product);

        // Act  
        var result = await _controller.GetProductById(productId);

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task GetProductById_ProductDoesNotExist_ReturnsNotFound()
    {
        // Arrange  
        var productId = Guid.NewGuid();

        _mockProductService
            .Setup(service => service.GetProductByIdAsync(productId))
            .ReturnsAsync((Product)null);

        // Act  
        var result = await _controller.GetProductById(productId);

        // Assert  
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Product not found.", notFoundResult.Value);
    }
}
