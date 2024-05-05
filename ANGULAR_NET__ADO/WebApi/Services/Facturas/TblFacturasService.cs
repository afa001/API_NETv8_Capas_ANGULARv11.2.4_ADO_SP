using WebApi.Models;

public class TblFacturasService : ITblFacturasService
{
    private readonly ITblFacturasRepository _tblFacturasRepository;

    public TblFacturasService(ITblFacturasRepository tblFacturasRepository)
    {
        _tblFacturasRepository = tblFacturasRepository ?? throw new ArgumentNullException(nameof(tblFacturasRepository));
    }

    public IEnumerable<TblFacturas> GetAllFacturas()
    {
        return _tblFacturasRepository.GetAll();
    }

    public TblFacturas GetFacturaById(int id)
    {
        return _tblFacturasRepository.GetById(id);
    }

    public TblFacturas AddFactura(TblFacturas factura)
    {
        return _tblFacturasRepository.Add(factura);
    }

    public void UpdateFactura(TblFacturas factura)
    {
        _tblFacturasRepository.Update(factura);
    }

    public void DeleteFactura(int id)
    {
        _tblFacturasRepository.Delete(id);
    }

    public IEnumerable<TblFacturas> GetFacturasByClienteId(int id)
    {
        return _tblFacturasRepository.GetFacturasByClienteId(id);
    }

    public IEnumerable<TblFacturas> GetFacturasByNumeroFactura(int numeroFactura)
    {
        return _tblFacturasRepository.GetFacturasByNumeroFactura(numeroFactura);
    }
}
