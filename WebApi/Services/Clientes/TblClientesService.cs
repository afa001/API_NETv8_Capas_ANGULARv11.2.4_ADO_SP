using WebApi.Models;

public class TblClientesService : ITblClientesService
{
    private readonly ITblClientesRepository _tblClientesRepository;

    public TblClientesService(ITblClientesRepository tblClientesRepository)
    {
        _tblClientesRepository = tblClientesRepository ?? throw new ArgumentNullException(nameof(tblClientesRepository));
    }

    public IEnumerable<TblClientes> GetAllClientes()
    {
        return _tblClientesRepository.GetAll();
    }

    public TblClientes GetClienteById(int id)
    {
        return _tblClientesRepository.GetById(id);
    }

    public void AddCliente(TblClientes cliente)
    {
        _tblClientesRepository.Add(cliente);
    }

    public void UpdateCliente(TblClientes cliente)
    {
        _tblClientesRepository.Update(cliente);
    }

    public void DeleteCliente(int id)
    {
        _tblClientesRepository.Delete(id);
    }
}
