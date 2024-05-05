using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class FacturasController : ControllerBase
{
    private readonly ITblFacturasService _tblFacturasService;

    public FacturasController(ITblFacturasService tblFacturasService)
    {
        _tblFacturasService = tblFacturasService;
    }

    [HttpGet]
    public IActionResult GetAllFacturas()
    {
        var facturas = _tblFacturasService.GetAllFacturas();
        return Ok(facturas);
    }

    [HttpGet("{id}")]
    public IActionResult GetFacturaById(int id)
    {
        var factura = _tblFacturasService.GetFacturaById(id);
        if (factura == null)
        {
            return NotFound();
        }
        return Ok(factura);
    }

    [HttpGet("GetFacturasByCliente/{id}")]
    public IActionResult GetFacturasByClienteId(int id)
    {
        var factura = _tblFacturasService.GetFacturasByClienteId(id);
        if (factura == null)
        {
            return NotFound();
        }
        return Ok(factura);
    }

    [HttpGet("GetFacturasByNumeroFactura/{numeroFactura}")]
    public IActionResult GetFacturasByNumeroFactura(int numeroFactura)
    {
        var factura = _tblFacturasService.GetFacturasByNumeroFactura(numeroFactura);
        if (factura == null)
        {
            return NotFound();
        }
        return Ok(factura);
    }

    [HttpPost]
    public IActionResult AddFactura(TblFacturas factura)
    {
        var newFactura = _tblFacturasService.AddFactura(factura);
        return CreatedAtAction(nameof(GetFacturaById), new { id = factura.Id }, new
        {
            id = newFactura.Id,
            message = "Factura creada con éxito",
            statusCode = StatusCodes.Status201Created
        });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFactura(int id, TblFacturas factura)
    {
        if (id != factura.Id)
        {
            return BadRequest();
        }

        _tblFacturasService.UpdateFactura(factura);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFactura(int id)
    {
        _tblFacturasService.DeleteFactura(id);
        return NoContent();
    }
}
