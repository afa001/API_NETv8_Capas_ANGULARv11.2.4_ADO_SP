using WebApi.Models;

public interface ITblDetallesFacturaService
{
    IEnumerable<TblDetallesFactura> GetAllDetallesFactura();
    TblDetallesFactura GetDetalleFacturaById(int id);
    void AddDetalleFactura(TblDetallesFactura detalleFactura);
    void UpdateDetalleFactura(TblDetallesFactura detalleFactura);
    void DeleteDetalleFactura(int id);
}

