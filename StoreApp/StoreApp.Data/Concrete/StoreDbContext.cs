using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
                new List<Product>
                {
                new () {Id = 1, Category = "Category#1", Name = "Product#1", Description = "Product#1Description", Price = 9999 },
                new () {Id = 2, Category = "Category#2", Name = "Product#2", Description = "Product#2Description", Price = 9999 },
                new() { Id = 3, Category = "Category#3", Name = "Product#3", Description = "Product#3Description", Price = 9999 },
                new() { Id = 4, Category = "Category#4", Name = "Product#4", Description = "Product#4Description", Price = 9999 },
                new() { Id = 5, Category = "Category#5", Name = "Product#5", Description = "Product#5Description", Price = 9999 },
                new() { Id = 6, Category = "Category#6", Name = "Product#6", Description = "Product#6Description", Price = 9999 },
                }
            );
    }
}