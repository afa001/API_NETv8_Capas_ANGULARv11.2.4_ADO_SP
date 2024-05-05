using WebApi.Models;

public interface ITblDetallesFacturaRepository
{
    IEnumerable<TblDetallesFactura> GetAll();
    TblDetallesFactura GetById(int id);
    void Add(TblDetallesFactura detalleFactura);
    void Update(TblDetallesFactura detalleFactura);
    void Delete(int id);
}

