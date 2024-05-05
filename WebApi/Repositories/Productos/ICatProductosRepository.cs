using WebApi.Models;

public interface ICatProductosRepository
{
    IEnumerable<CatProductos> GetAll();
    CatProductos GetById(int id);
    void Add(CatProductos producto);
    void Update(CatProductos producto);
    void Delete(int id);
}

