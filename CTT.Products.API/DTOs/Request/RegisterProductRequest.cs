namespace CTT.Products.API.DTOs.Requests;

public class RegisterProductRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
}
