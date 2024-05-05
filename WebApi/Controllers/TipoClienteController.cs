using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class TipoClienteController : ControllerBase
{
    //private readonly ICatTipoClienteService _catTipoClienteService;

    //public TipoClienteController(ICatTipoClienteService catTipoClienteService)
    //{
    //    _catTipoClienteService = catTipoClienteService;
    //}

    //[HttpGet]
    //public IActionResult GetAllTipoClientes()
    //{
    //    var tipoClientes = _catTipoClienteService.GetAllTiposCliente();
    //    return Ok(tipoClientes);
    //}

    //[HttpGet("{id}")]
    //public IActionResult GetTipoClienteById(int id)
    //{
    //    var tipoCliente = _catTipoClienteService.GetTipoClienteById(id);
    //    if (tipoCliente == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(tipoCliente);
    //}

    //[HttpPost]
    //public IActionResult AddTipoCliente(CatTipoCliente tipoCliente)
    //{
    //    _catTipoClienteService.AddTipoCliente(tipoCliente);
    //    return CreatedAtAction(nameof(GetTipoClienteById), new { id = tipoCliente.Id }, tipoCliente);
    //}

    //[HttpPut("{id}")]
    //public IActionResult UpdateTipoCliente(int id, CatTipoCliente tipoCliente)
    //{
    //    if (id != tipoCliente.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _catTipoClienteService.UpdateTipoCliente(tipoCliente);
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public IActionResult DeleteTipoCliente(int id)
    //{
    //    _catTipoClienteService.DeleteTipoCliente(id);
    //    return NoContent();
    //}

}
