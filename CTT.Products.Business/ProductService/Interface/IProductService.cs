using Products.Domain;

namespace CTT.Products.Business.ProductService.Interface;

public interface IProductService
{
    Task<Product> RegisterProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}