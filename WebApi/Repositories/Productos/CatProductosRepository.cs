using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

public class CatProductosRepository : ICatProductosRepository
{
    private readonly string _connectionString;

    public CatProductosRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<CatProductos> GetAll()
    {
        List<CatProductos> productos = new List<CatProductos>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetAllProductos", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new CatProductos
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            ImagenProducto = reader["ImagenProducto"] == DBNull.Value ? null : (byte[])reader["ImagenProducto"],
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                            Ext = reader["Ext"].ToString()
                        });
                    }
                }
            }
        }

        return productos;
    }

    public CatProductos GetById(int id)
    {
        CatProductos producto = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetProductoById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    producto = new CatProductos
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        NombreProducto = reader["NombreProducto"].ToString(),
                        ImagenProducto = reader["ImagenProducto"] == DBNull.Value ? null : (byte[])reader["ImagenProducto"],
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                        Ext = reader["Ext"].ToString()
                    };
                }
                reader.Close();
            }
        }

        return producto;
    }

    public void Add(CatProductos producto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("AddProducto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                command.Parameters.AddWithValue("@ImagenProducto", producto.ImagenProducto);
                command.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                command.Parameters.AddWithValue("@Ext", producto.Ext);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(CatProductos producto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("UpdateProducto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                command.Parameters.AddWithValue("@ImagenProducto", producto.ImagenProducto);
                command.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                command.Parameters.AddWithValue("@Ext", producto.Ext);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("DeleteProducto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
