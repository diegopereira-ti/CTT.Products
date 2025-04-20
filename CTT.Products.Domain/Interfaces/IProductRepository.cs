using Products.Domain;

namespace CTT.Products.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
}
