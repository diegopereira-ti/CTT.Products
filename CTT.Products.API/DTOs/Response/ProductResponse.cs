namespace CTT.Products.API.DTOs.Response;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
}
