namespace Komas_API.Models;

public class Product
{
    public int ProductId { get; set; }

    public required string ProductName { get; set; }

    public required string Description { get; set; }

    public required double Price { get; set; }

    public ICollection<Category>? Categories;
}