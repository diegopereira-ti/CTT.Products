using CTT.Products.Domain.Interfaces;
using Products.Domain;
using CTT.Products.Infrastructure;

namespace CTT.Products.Tests.Integration;

public class ProductRepositoryTests
{
    private readonly IProductRepository _productRepository;

    public ProductRepositoryTests()
    {
        var context = new MongoDbContext();
        _productRepository = new MongoProductRepository(context);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange  
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Description = "Test Product",
            Price = 10.99m,
            Stock = 100,
            Categories = new List<string> { "Category1", "Category2" }
        };
        await _productRepository.AddProductAsync(product);

        // Act  
        var result = await _productRepository.GetProductByIdAsync(productId);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetProductsAsync_ShouldReturnAllProducts()
    {
        // Arrange  
        var product1 = new Product
        {
            Id = Guid.NewGuid(),
            Description = "Product 1",
            Price = 5.99m,
            Stock = 50,
            Categories = new List<string> { "Category1" }
        };
        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            Description = "Product 2",
            Price = 15.99m,
            Stock = 30,
            Categories = new List<string> { "Category2" }
        };
        await _productRepository.AddProductAsync(product1);
        await _productRepository.AddProductAsync(product2);

        // Act  
        var result = await _productRepository.GetProductsAsync();

        // Assert  
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        // Arrange  
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Description = "New Product",
            Price = 20.99m,
            Stock = 200,
            Categories = new List<string> { "Category3" }
        };

        // Act  
        await _productRepository.AddProductAsync(product);
        var result = await _productRepository.GetProductByIdAsync(product.Id);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task UpdateProductAsync_ShouldUpdateProduct()
    {
        // Arrange  
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Description = "Old Product",
            Price = 10.99m,
            Stock = 100,
            Categories = new List<string> { "Category1" }
        };
        await _productRepository.AddProductAsync(product);

        product.Description = "Updated Product";
        product.Price = 15.99m;

        // Act  
        await _productRepository.UpdateProductAsync(product);
        var result = await _productRepository.GetProductByIdAsync(product.Id);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal("Updated Product", result.Description);
        Assert.Equal(15.99m, result.Price);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task DeleteProductAsync_ShouldRemoveProduct()
    {
        // Arrange  
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Description = "Product to Delete",
            Price = 9.99m,
            Stock = 10,
            Categories = new List<string> { "Category1" }
        };
        await _productRepository.AddProductAsync(product);

        // Act  
        await _productRepository.DeleteProductAsync(product.Id);
        var result = await _productRepository.GetProductByIdAsync(product.Id);

        // Assert  
        Assert.Null(result);
    }
}
