using Komas_API.Data;
using Komas_API.Models;

namespace Komas_API.Services;

public interface IProductService
{
    public List<Product> GetAll();
    public Product? GetById(int id);

    public Product? CreateProduct(ProductDto newProduct);

    public Product? UpdateById(int id, ProductDto product);

    public bool DeleteById(int id);

    PaginatedResponse<Product> GetPaginated(int pageNumber, int pageSize); 
}

public class ProductService : IProductService
{
    private readonly ProductDbContext _context;
    public ProductService(ProductDbContext context)
    {
        _context = context;
    }
    private readonly List<Product> _products = new();
    public List<Product> GetAll()
    {
        return _context.Products.OrderBy(p => p.ProductId).ToList();
    }

    public Product? GetById(int id)
    {
        Product? product;
        product = _context.Products.FirstOrDefault<Product>(p => p.ProductId == id);
        return product;
    }

    public Product? CreateProduct(ProductDto newProduct)
    {
        if (newProduct.ProductName != null && newProduct.Description != null)
        {

            Product product = new() { ProductName = newProduct.ProductName, Description = newProduct.Description, Price = newProduct.Price };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        else
        {
            return null;
        }
    }

    public Product? UpdateById(int id, ProductDto product)
    {
        Product? productToUpdate = _context.Products.SingleOrDefault(p => p.ProductId == id);
        if (productToUpdate == null)
        {
            return null;
        }
        productToUpdate.Categories = product.Categories;
        if (product.ProductName != null && product.ProductName.Trim() != "")
        {
            productToUpdate.ProductName = product.ProductName;
        }
        if (product.Description != null && product.Description.Trim() != "")
        {
            productToUpdate.Description = product.Description;
        }
        if (product.Price != 0)
        {
            productToUpdate.Price = product.Price;
        }
        _context.SaveChanges();
        return productToUpdate;
    }

    public bool DeleteById(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return false;
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }

    public PaginatedResponse<Product> GetPaginated(int pageNumber, int pageSize)
    {
        var totalCount = _context.Products.Count();
        var items = _context.Products
            .OrderBy(p => p.ProductId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResponse<Product>(items, totalCount, pageNumber, pageSize);
    }
}