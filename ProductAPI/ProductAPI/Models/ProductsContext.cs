using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, ProductName = "Iphone 14", Price = 10000, IsActive = true }
        );
        modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, ProductName = "Iphone 15", Price = 20000, IsActive = true }
        );
        modelBuilder.Entity<Product>().HasData(new Product { ProductId = 3, ProductName = "Iphone 16", Price = 30000, IsActive = true }
        );
        modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4, ProductName = "Iphone SE", Price = 40000, IsActive = true }
        );
    }
    public DbSet<Product> Products { get; set; }
}