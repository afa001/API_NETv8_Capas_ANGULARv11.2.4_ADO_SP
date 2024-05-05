using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClientesController : ControllerBase
{
    private readonly ITblClientesService _tblClientesService;

    public ClientesController(ITblClientesService tblClientesService)
    {
        _tblClientesService = tblClientesService;
    }

    [HttpGet]
    public IActionResult GetAllClientes()
    {
        var clientes = _tblClientesService.GetAllClientes();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public IActionResult GetClienteById(int id)
    {
        var cliente = _tblClientesService.GetClienteById(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    [HttpPost]
    public IActionResult AddCliente(TblClientes cliente)
    {
        _tblClientesService.AddCliente(cliente);
        return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCliente(int id, TblClientes cliente)
    {
        if (id != cliente.Id)
        {
            return BadRequest();
        }

        _tblClientesService.UpdateCliente(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCliente(int id)
    {
        _tblClientesService.DeleteCliente(id);
        return NoContent();
    }
}
