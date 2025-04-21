using CTT.Products.Business.ProductService.Interface;
using CTT.Products.Domain.Interfaces;
using Products.Domain;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> RegisterProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Description))
        {
            throw new ArgumentException("Product description cannot be empty.");
        }

        await _productRepository.AddProductAsync(product);
        return product;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetProductsAsync();
    }
}