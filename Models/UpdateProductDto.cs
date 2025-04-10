namespace Komas_API.Models;

public class ProductDto
{
    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public ICollection<Category>? Categories;
}