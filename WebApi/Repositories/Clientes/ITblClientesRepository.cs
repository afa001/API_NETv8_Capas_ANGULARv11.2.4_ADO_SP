using WebApi.Models;

public interface ITblClientesRepository
{
    IEnumerable<TblClientes> GetAll();
    TblClientes GetById(int id);
    void Add(TblClientes cliente);
    void Update(TblClientes cliente);
    void Delete(int id);
}

