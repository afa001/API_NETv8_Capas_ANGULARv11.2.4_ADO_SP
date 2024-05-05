using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

public class TblClientesRepository : ITblClientesRepository
{
    private readonly string _connectionString;

    public TblClientesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<TblClientes> GetAll()
    {
        List<TblClientes> clientes = new List<TblClientes>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetAllClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new TblClientes
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                RazonSocial = Convert.ToString(reader["RazonSocial"]),
                                IdTipoCliente = Convert.ToInt32(reader["IdTipoCliente"]),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                RFC = Convert.ToString(reader["RFC"])
                            });
                        }
                    }
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        return clientes;
    }

    public TblClientes GetById(int id)
    {
        TblClientes cliente = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetClienteById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        cliente = new TblClientes
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RazonSocial = Convert.ToString(reader["RazonSocial"]),
                            IdTipoCliente = Convert.ToInt32(reader["IdTipoCliente"]),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            RFC = Convert.ToString(reader["RFC"])
                        };
                    }
                    reader.Close();
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        return cliente;
    }

    public void Add(TblClientes cliente)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RazonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@IdTipoCliente", cliente.IdTipoCliente);
                    command.Parameters.AddWithValue("@FechaCreacion", cliente.FechaCreacion);
                    command.Parameters.AddWithValue("@RFC", cliente.RFC);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }

    public void Update(TblClientes cliente)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("UpdateCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@RazonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@IdTipoCliente", cliente.IdTipoCliente);
                    command.Parameters.AddWithValue("@FechaCreacion", cliente.FechaCreacion);
                    command.Parameters.AddWithValue("@RFC", cliente.RFC);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DeleteCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }
}