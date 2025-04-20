using Products.Domain;

namespace CTT.Products.Tests;

public class ProductTests
{
    [Fact]
    public void Product_Should_Have_Valid_Description()
    {
        // Arrange  
        var product = new Product
        {
            Description = "Valid Product Description"
        };

        // Act  
        var isValid = !string.IsNullOrWhiteSpace(product.Description);

        // Assert  
        Assert.True(isValid, "Product description should be valid and not empty.");
    }

    [Fact]
    public void Product_Should_Calculate_TotalStock()
    {
        // Arrange  
        var product = new Product
        {
            Stock = 10
        };
        // Act  
        var totalStock = product.Stock;
        // Assert  
        Assert.Equal(10, totalStock);
    }

    [Fact]
    public void Product_Should_Fail_When_Price_Is_Invalid()
    {
        // Arrange  
        var product = new Product
        {
            Price = -10.0m
        };
        // Act  
        var isValid = product.Price > 0;
        // Assert  
        Assert.False(isValid, "Product price should be greater than zero.");
    }

    [Fact]
    public void Product_Should_Have_Default_Id()
    {
        // Arrange  
        var product = new Product();

        // Act  
        var hasDefaultId = product.Id != Guid.Empty;

        // Assert  
        Assert.True(hasDefaultId, "Product should have a default non-empty GUID as Id.");
    }

    [Fact]
    public void Product_Should_Allow_Empty_Categories()
    {
        // Arrange  
        var product = new Product();

        // Act  
        var categories = product.Categories;

        // Assert  
        Assert.NotNull(categories);
        Assert.Empty(categories);
    }

    [Fact]
    public void Product_Should_Allow_Valid_Price()
    {
        // Arrange  
        var product = new Product
        {
            Price = 100.0m
        };

        // Act  
        var isValid = product.Price > 0;

        // Assert  
        Assert.True(isValid, "Product price should be valid and greater than zero.");
    }

    [Fact]
    public void Product_Should_Update_Stock()
    {
        // Arrange  
        var product = new Product
        {
            Stock = 5
        };

        // Act  
        product.Stock += 10;

        // Assert  
        Assert.Equal(15, product.Stock);
    }
}