using CTT.Products.Domain.Interfaces;
using MongoDB.Driver;
using Products.Domain;

namespace CTT.Products.Infrastructure;

public class MongoProductRepository : IProductRepository
{

    private readonly IMongoCollection<ProductDocument> _productsCollection;

    public MongoProductRepository(MongoDbContext context)
    {
        _productsCollection = context.Products;
    }

    public async Task AddProductAsync(Product product)
    {
        var productDocument = ProductDocument.FromDomain(product);
        await _productsCollection.InsertOneAsync(productDocument);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _productsCollection.DeleteOneAsync(p => p.Id == id);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var doc = await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        return doc?.ToDomain();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var productDocuments = await _productsCollection.Find(_ => true).ToListAsync();
        return productDocuments.Select(doc => doc.ToDomain());
    }

    public async Task UpdateProductAsync(Product product)
    {
        var filter = Builders<ProductDocument>.Filter.Eq(p => p.Id, product.Id);
        var update = Builders<ProductDocument>.Update
            .Set(p => p.Description, product.Description)
            .Set(p => p.Price, product.Price)
            .Set(p => p.Stock, product.Stock)
            .Set(p => p.Categories, product.Categories);

        await _productsCollection.UpdateOneAsync(filter, update);
    }
}
