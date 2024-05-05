using WebApi.Models;

public interface ITblFacturasService
{
    IEnumerable<TblFacturas> GetAllFacturas();
    IEnumerable<TblFacturas> GetFacturasByClienteId(int id);
    IEnumerable<TblFacturas> GetFacturasByNumeroFactura(int id);
    TblFacturas GetFacturaById(int id);
    TblFacturas AddFactura(TblFacturas factura);
    void UpdateFactura(TblFacturas factura);
    void DeleteFactura(int id);
}
