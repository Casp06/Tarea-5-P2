using Microsoft.AspNetCore.Mvc;
using tarea5P2.Models;

namespace tarea5P2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 10.5M, Stock = 10 },
        new Product { Id = 2, Name = "Product 2", Price = 15.0M, Stock = 5 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get() => _products;

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public ActionResult Post([FromBody] Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null) return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        _products.Remove(product);
        return NoContent();
    }
}
