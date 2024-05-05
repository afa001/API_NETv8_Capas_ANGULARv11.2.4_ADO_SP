using WebApi.Models;

public interface ITblClientesService
{
    IEnumerable<TblClientes> GetAllClientes();
    TblClientes GetClienteById(int id);
    void AddCliente(TblClientes cliente);
    void UpdateCliente(TblClientes cliente);
    void DeleteCliente(int id);
}
