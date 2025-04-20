using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Products.Domain;

public class ProductDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Categories { get; set; } = new();
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public static ProductDocument FromDomain(Product product)
    {
        return new ProductDocument
        {
            Id = product.Id,
            Description = product.Description,
            Categories = product.Categories,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public Product ToDomain()
    {
        return new Product(
            id: this.Id,
            description: this.Description,
            price: this.Price,
            stock: this.Stock,
            categories: this.Categories
        );
    }
}
