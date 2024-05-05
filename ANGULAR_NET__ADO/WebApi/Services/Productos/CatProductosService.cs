using WebApi.Models;

public class CatProductosService : ICatProductosService
{
    private readonly ICatProductosRepository _catProductosRepository;

    public CatProductosService(ICatProductosRepository catProductosRepository)
    {
        _catProductosRepository = catProductosRepository ?? throw new ArgumentNullException(nameof(catProductosRepository));
    }

    public IEnumerable<CatProductos> GetAllProductos()
    {
        return _catProductosRepository.GetAll();
    }

    public CatProductos GetProductoById(int id)
    {
        return _catProductosRepository.GetById(id);
    }

    public void AddProducto(CatProductos producto)
    {
        _catProductosRepository.Add(producto);
    }

    public void UpdateProducto(CatProductos producto)
    {
        _catProductosRepository.Update(producto);
    }

    public void DeleteProducto(int id)
    {
        _catProductosRepository.Delete(id);
    }
}
