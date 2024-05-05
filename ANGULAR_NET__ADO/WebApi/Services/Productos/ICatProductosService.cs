using WebApi.Models;

public interface ICatProductosService
{
    IEnumerable<CatProductos> GetAllProductos();
    CatProductos GetProductoById(int id);
    void AddProducto(CatProductos producto);
    void UpdateProducto(CatProductos producto);
    void DeleteProducto(int id);
}
