using WebApi.Models;

public class TblDetallesFacturaService : ITblDetallesFacturaService
{
    private readonly ITblDetallesFacturaRepository _tblDetallesFacturaRepository;

    public TblDetallesFacturaService(ITblDetallesFacturaRepository tblDetallesFacturaRepository)
    {
        _tblDetallesFacturaRepository = tblDetallesFacturaRepository ?? throw new ArgumentNullException(nameof(tblDetallesFacturaRepository));
    }

    public IEnumerable<TblDetallesFactura> GetAllDetallesFactura()
    {
        return _tblDetallesFacturaRepository.GetAll();
    }

    public TblDetallesFactura GetDetalleFacturaById(int id)
    {
        return _tblDetallesFacturaRepository.GetById(id);
    }

    public void AddDetalleFactura(TblDetallesFactura detalleFactura)
    {
        _tblDetallesFacturaRepository.Add(detalleFactura);
    }

    public void UpdateDetalleFactura(TblDetallesFactura detalleFactura)
    {
        _tblDetallesFacturaRepository.Update(detalleFactura);
    }

    public void DeleteDetalleFactura(int id)
    {
        _tblDetallesFacturaRepository.Delete(id);
    }
}
