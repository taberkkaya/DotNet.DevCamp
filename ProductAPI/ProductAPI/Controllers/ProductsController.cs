using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DTO;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

//localhost:5000/api/products

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products
        .Where(i => i.IsActive)
        .Select(p => ProductToDTO(p))
        .ToListAsync();

        return Ok(products);
    }

    //localhost:5000/api/products/1 => GET
    // [HttpGet("api/[controller]/{id}")]
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var p = await _context.Products
        .Where(i => i.ProductId == id)
        .Select(p => ProductToDTO(p))
        .FirstOrDefaultAsync();

        if (p == null)
        {
            return NotFound();
        }

        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product entity)
    {
        if (id != entity.ProductId)
        {
            return BadRequest();
        }

        var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        product.ProductName = entity.ProductName;
        product.Price = entity.Price;
        product.IsActive = entity.IsActive;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static ProductDTO ProductToDTO(Product p)
    {
        var entity = new ProductDTO();
        if (p != null)
        {
            entity.ProductId = p.ProductId;
            entity.Price = p.Price;
            entity.ProductName = p.ProductName;
        }
        return entity;
    }
}