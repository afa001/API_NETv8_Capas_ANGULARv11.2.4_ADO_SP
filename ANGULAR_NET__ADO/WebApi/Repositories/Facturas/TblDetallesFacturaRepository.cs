using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

public class TblDetallesFacturaRepository : ITblDetallesFacturaRepository
{
    private readonly string _connectionString;

    public TblDetallesFacturaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<TblDetallesFactura> GetAll()
    {
        List<TblDetallesFactura> detallesFactura = new List<TblDetallesFactura>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetAllDetallesFactura", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detallesFactura.Add(new TblDetallesFactura
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdFactura = Convert.ToInt32(reader["IdFactura"]),
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            CantidadDeProducto = Convert.ToInt32(reader["CantidadDeProducto"]),
                            PrecioUnitarioProducto = Convert.ToDecimal(reader["PrecioUnitarioProducto"]),
                            SubtotalProducto = Convert.ToDecimal(reader["SubtotalProducto"]),
                            Notas = reader["Notas"].ToString()
                        });
                    }
                }
            }
        }

        return detallesFactura;
    }

    public TblDetallesFactura GetById(int id)
    {
        TblDetallesFactura detalleFactura = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetDetalleFacturaById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    detalleFactura = new TblDetallesFactura
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        IdFactura = Convert.ToInt32(reader["IdFactura"]),
                        IdProducto = Convert.ToInt32(reader["IdProducto"]),
                        CantidadDeProducto = Convert.ToInt32(reader["CantidadDeProducto"]),
                        PrecioUnitarioProducto = Convert.ToDecimal(reader["PrecioUnitarioProducto"]),
                        SubtotalProducto = Convert.ToDecimal(reader["SubtotalProducto"]),
                        Notas = reader["Notas"].ToString()
                    };
                }
                reader.Close();
            }
        }

        return detalleFactura;
    }

    public void Add(TblDetallesFactura detalleFactura)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("AddDetalleFactura", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdFactura", detalleFactura.IdFactura);
                command.Parameters.AddWithValue("@IdProducto", detalleFactura.IdProducto);
                command.Parameters.AddWithValue("@CantidadDeProducto", detalleFactura.CantidadDeProducto);
                command.Parameters.AddWithValue("@PrecioUnitarioProducto", detalleFactura.PrecioUnitarioProducto);
                command.Parameters.AddWithValue("@SubtotalProducto", detalleFactura.SubtotalProducto);
                command.Parameters.AddWithValue("@Notas", detalleFactura.Notas);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(TblDetallesFactura detalleFactura)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("UpdateDetalleFactura", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", detalleFactura.Id);
                command.Parameters.AddWithValue("@IdFactura", detalleFactura.IdFactura);
                command.Parameters.AddWithValue("@IdProducto", detalleFactura.IdProducto);
                command.Parameters.AddWithValue("@CantidadDeProducto", detalleFactura.CantidadDeProducto);
                command.Parameters.AddWithValue("@PrecioUnitarioProducto", detalleFactura.PrecioUnitarioProducto);
                command.Parameters.AddWithValue("@SubtotalProducto", detalleFactura.SubtotalProducto);
                command.Parameters.AddWithValue("@Notas", detalleFactura.Notas);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("DeleteDetalleFactura", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
