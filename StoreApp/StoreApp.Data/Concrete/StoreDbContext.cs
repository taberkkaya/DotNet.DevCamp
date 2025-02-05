using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(p => p.Products)
            .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
            .HasIndex(u => u.Url)
            .IsUnique();

        modelBuilder.Entity<Product>().HasData(
                new List<Product>
                {
                new () {Id = 1, Name = "Product#1", Description = "Product#1Description", Price = 9999 },
                new () {Id = 2, Name = "Product#2", Description = "Product#2Description", Price = 9999 },
                new() { Id = 3, Name = "Product#3", Description = "Product#3Description", Price = 9999 },
                new() { Id = 4, Name = "Product#4", Description = "Product#4Description", Price = 9999 },
                new() { Id = 5, Name = "Product#5", Description = "Product#5Description", Price = 9999 },
                new() { Id = 6, Name = "Product#6", Description = "Product#6Description", Price = 9999 },
                }
            );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>
            {
                new() {Id = 1, Name = "Category#1", Url = "category-1"},
                new() {Id = 2, Name = "Category#2", Url = "category-2"},
                new() {Id = 3, Name = "Category#3", Url = "category-3"},
                new() {Id = 4, Name = "Category#4", Url = "category-4"}
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>{
                new() {ProductId = 1, CategoryId = 1},
                new() {ProductId = 2, CategoryId = 1},
                new() {ProductId = 3, CategoryId = 2},
                new() {ProductId = 4, CategoryId = 3},
                new() {ProductId = 5, CategoryId = 4},
                new() {ProductId = 6, CategoryId = 4},
            }
        );
    }
}