using WebApi.Models;

public interface ITblFacturasRepository
{
    IEnumerable<TblFacturas> GetAll();
    IEnumerable<TblFacturas> GetFacturasByClienteId(int id);
    IEnumerable<TblFacturas> GetFacturasByNumeroFactura(int numeroFactura);
    TblFacturas GetById(int id);
    TblFacturas Add(TblFacturas factura);
    void Update(TblFacturas factura);
    void Delete(int id);
}

