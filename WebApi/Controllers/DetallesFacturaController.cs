using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
[Route("api/[controller]")]
[ApiController]
public class DetallesFacturaController : ControllerBase
{
    private readonly ITblDetallesFacturaService _tblDetallesFacturaService;

    public DetallesFacturaController(ITblDetallesFacturaService tblDetallesFacturaService)
    {
        _tblDetallesFacturaService = tblDetallesFacturaService;
    }

    [HttpGet]
    public IActionResult GetAllDetallesFactura()
    {
        var detallesFactura = _tblDetallesFacturaService.GetAllDetallesFactura();
        return Ok(detallesFactura);
    }

    [HttpGet("{id}")]
    public IActionResult GetDetalleFacturaById(int id)
    {
        var detalleFactura = _tblDetallesFacturaService.GetDetalleFacturaById(id);
        if (detalleFactura == null)
        {
            return NotFound();
        }
        return Ok(detalleFactura);
    }

    [HttpPost]
    public IActionResult AddDetalleFactura(TblDetallesFactura detalleFactura)
    {
        _tblDetallesFacturaService.AddDetalleFactura(detalleFactura);
        return CreatedAtAction(nameof(GetDetalleFacturaById), new { id = detalleFactura.Id }, detalleFactura);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDetalleFactura(int id, TblDetallesFactura detalleFactura)
    {
        if (id != detalleFactura.Id)
        {
            return BadRequest();
        }

        _tblDetallesFacturaService.UpdateDetalleFactura(detalleFactura);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDetalleFactura(int id)
    {
        _tblDetallesFacturaService.DeleteDetalleFactura(id);
        return NoContent();
    }
}

