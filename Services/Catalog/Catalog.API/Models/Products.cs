namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public List<string> Category { get; set; } = new();

    public string Descriptions { get; set; } = default!;

    public string ImageFiles { get; set; } = default!;

    public decimal Price { get; set; }
}
