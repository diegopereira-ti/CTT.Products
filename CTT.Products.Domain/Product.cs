namespace Products.Domain;

public record Product
{
    public Product(Guid id, string description, decimal price, int stock, List<string> categories)
    {
        Id = id;
        Description = description;
        Price = price;
        Stock = stock;
        this.categories = categories ?? new List<string>();
    }

    public Product()
    {
        categories = new List<string>();
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    private List<string> categories;

    public List<string> Categories
    {
        get => categories;
        set => categories = value ?? new List<string>();
    }
}
