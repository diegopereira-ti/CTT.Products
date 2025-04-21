using CTT.Products.Domain.Interfaces;
using Moq;
using Products.Domain;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange  
        var productId = Guid.NewGuid();
        var expectedProduct = new Product { Id = productId, Description = "Test Product" };
        _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        // Act  
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Id, result?.Id);
        Assert.Equal(expectedProduct.Description, result?.Description);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange  
        var productId = Guid.NewGuid();
        _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        // Act  
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert  
        Assert.Null(result);
    }

    [Fact]
    public async Task RegisterProductAsync_ShouldThrowException_WhenDescriptionIsEmpty()
    {
        // Arrange  
        var product = new Product { Description = "" };

        // Act & Assert  
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.RegisterProductAsync(product));
        Assert.Equal("Product description cannot be empty.", exception.Message);
    }

    [Fact]
    public async Task RegisterProductAsync_ShouldAddProduct_WhenValidProductIsProvided()
    {
        // Arrange  
        var product = new Product { Id = Guid.NewGuid(), Description = "Valid Product" };

        _productRepositoryMock.Setup(repo => repo.AddProductAsync(product))
            .Returns(Task.CompletedTask);

        // Act  
        var result = await _productService.RegisterProductAsync(product);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Description, result.Description);
        _productRepositoryMock.Verify(repo => repo.AddProductAsync(product), Times.Once);
    }
}
