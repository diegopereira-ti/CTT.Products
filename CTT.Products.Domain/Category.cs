namespace CTT.Products.Domain;

public record Category
{
    private Guid guid;
    private string v;

    public Category(Guid guid, string v)
    {
        this.guid = guid;
        this.v = v;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
}
