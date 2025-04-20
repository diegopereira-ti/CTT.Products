namespace Products.Domain;

public record Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string[] Categories { get; set; } = [];
}
