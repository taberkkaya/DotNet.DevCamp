using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

//localhost:5000/api/products
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private static List<Product>? _products;

    public ProductsController()
    {
        _products =
        [
            new Product { ProductId = 1, ProductName = "Iphone 14", Price = 10000, IsActive = true },
            new Product { ProductId = 2, ProductName = "Iphone 15", Price = 20000, IsActive = true },
            new Product { ProductId = 3, ProductName = "Iphone 16", Price = 30000, IsActive = true },
            new Product { ProductId = 4, ProductName = "Iphone SE", Price = 40000, IsActive = true },
            new Product { ProductId = 5, ProductName = "Iphone 5", Price = 50000, IsActive = true },
        ];
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        if (_products == null)
        {
            return NotFound();
        }
        return Ok(_products);
    }

    //localhost:5000/api/products/1 => GET
    // [HttpGet("api/[controller]/{id}")]
    [HttpGet("{id}")]
    public IActionResult GetProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var p = _products?.FirstOrDefault(i => i.ProductId == id);

        if (p == null)
        {
            return NotFound();
        }

        return Ok(p);
    }
}