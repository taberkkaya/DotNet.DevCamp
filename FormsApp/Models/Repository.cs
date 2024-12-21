using FormsApp.Models;

namespace FormsApp.Model;

public class Repository
{
    private static readonly List<Product> _products = new();
    public static List<Product> Products
    {
        get
        {
            return _products;
        }
    }
    private static readonly List<Category> _categories = new();
    public static List<Category> Categories
    {
        get
        {
            return _categories;
        }
    }
    static Repository()
    {
        _categories.Add(new Category() { CategoryId = 1, Name = "Telefon" });
        _categories.Add(new Category() { CategoryId = 2, Name = "Bilgisayar" });

        _products.Add(new Product() { ProductId = 1, Name = "Iphone 14", Price = 40000, Image = "1.jpg", IsActive = true, CategoryId = 1 });
        _products.Add(new Product() { ProductId = 2, Name = "Iphone 15", Price = 50000, Image = "2.jpg", IsActive = true, CategoryId = 1 });
        _products.Add(new Product() { ProductId = 3, Name = "Iphone 16", Price = 60000, Image = "3.jpg", IsActive = true, CategoryId = 1 });
        _products.Add(new Product() { ProductId = 4, Name = "Iphone 17", Price = 70000, Image = "4.jpg", IsActive = true, CategoryId = 1 });
        _products.Add(new Product() { ProductId = 5, Name = "Macbook Air", Price = 80000, Image = "5.jpg", IsActive = true, CategoryId = 2 });
        _products.Add(new Product() { ProductId = 6, Name = "Macbook Pro", Price = 90000, Image = "6.jpg", IsActive = true, CategoryId = 2 });
    }

    public static void CreateProduct(Product entity)
    {
        _products.Add(entity);
    }

    public static void EditProduct(Product updatedProduct)
    {
        var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
        if (entity != null)
        {
            entity.Name = updatedProduct.Name;
            entity.Price = updatedProduct.Price;
            entity.Image = updatedProduct.Image;
            entity.IsActive = updatedProduct.IsActive;
            entity.CategoryId = updatedProduct.CategoryId;
        }
    }

    public static void DeleteProduct(Product deletedProduct)
    {
        var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);
        if (entity != null)
        {
            _products.Remove(entity);
        }
    }
}