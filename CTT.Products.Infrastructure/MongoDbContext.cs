using MongoDB.Driver;

namespace CTT.Products.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        var variables = Environment.GetEnvironmentVariables();
        var connectionString = Environment.GetEnvironmentVariable("MongoDb__ConnectionString")
                               ?? throw new InvalidOperationException("Environment variable 'MongoDb__ConnectionString' is not set.");
        var databaseName = Environment.GetEnvironmentVariable("MongoDb__DatabaseName")
                           ?? throw new InvalidOperationException("Environment variable 'MongoDb__DatabaseName' is not set.");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<ProductDocument> Products => _database.GetCollection<ProductDocument>("Products");
}
