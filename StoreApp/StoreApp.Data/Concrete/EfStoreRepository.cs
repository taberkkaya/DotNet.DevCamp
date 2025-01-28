using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EfStoreRepository : IStoreRepository
{
    private StoreDbContext _context;

    public EfStoreRepository(StoreDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products;

    public void CreateProduct(Product entity)
    {
        _context.Products.Add(entity);
        _context.SaveChanges();
    }
}