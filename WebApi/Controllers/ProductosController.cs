using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ICatProductosRepository _catProductosRepository;

    public ProductosController(ICatProductosRepository catProductosRepository)
    {
        _catProductosRepository = catProductosRepository;
    }

    [HttpGet]
    public IActionResult GetAllProductos()
    {
        var productos = _catProductosRepository.GetAll();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductoById(int id)
    {
        var producto = _catProductosRepository.GetById(id);
        if (producto == null)
        {
            return NotFound();
        }
        return Ok(producto);
    }

    [HttpPost]
    public IActionResult AddProducto(CatProductos producto)
    {
        _catProductosRepository.Add(producto);
        return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProducto(int id, CatProductos producto)
    {
        if (id != producto.Id)
        {
            return BadRequest();
        }

        _catProductosRepository.Update(producto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProducto(int id)
    {
        _catProductosRepository.Delete(id);
        return NoContent();
    }
}
