using Komas_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Komas_API.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData([new Product { ProductName = "AirForce max", Price = 30, Description = "I dont even know", ProductId = 1 }, new Product { ProductName = "mouse", Price = 89, Description = "Another Description", ProductId = 2 }]);
    }

}