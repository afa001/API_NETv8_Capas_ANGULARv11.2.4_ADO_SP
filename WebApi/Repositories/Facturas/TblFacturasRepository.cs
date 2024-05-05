using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using WebApi.Models;

public class TblFacturasRepository : ITblFacturasRepository
{
    private readonly string _connectionString;

    public TblFacturasRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<TblFacturas> GetAll()
    {
        List<TblFacturas> facturas = new List<TblFacturas>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetAllFacturas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(new TblFacturas
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FechaEmisionFactura = Convert.ToDateTime(reader["FechaEmisionFactura"]),
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                NumeroFactura = Convert.ToInt32(reader["NumeroFactura"]),
                                NumeroTotalArticulos = Convert.ToInt32(reader["NumeroTotalArticulos"]),
                                SubTotalFacturas = Convert.ToDecimal(reader["SubTotalFacturas"]),
                                TotalImpuestos = Convert.ToDecimal(reader["TotalImpuestos"]),
                                TotalFactura = Convert.ToDecimal(reader["TotalFactura"])
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

        return facturas;
    }

    public TblFacturas GetById(int id)
    {
        TblFacturas factura = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetFacturaById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        factura = new TblFacturas
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FechaEmisionFactura = Convert.ToDateTime(reader["FechaEmisionFactura"]),
                            IdCliente = Convert.ToInt32(reader["IdCliente"]),
                            NumeroFactura = Convert.ToInt32(reader["NumeroFactura"]),
                            NumeroTotalArticulos = Convert.ToInt32(reader["NumeroTotalArticulos"]),
                            SubTotalFacturas = Convert.ToDecimal(reader["SubTotalFacturas"]),
                            TotalImpuestos = Convert.ToDecimal(reader["TotalImpuestos"]),
                            TotalFactura = Convert.ToDecimal(reader["TotalFactura"])
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

        return factura;
    }

    public TblFacturas Add(TblFacturas factura)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddFactura", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FechaEmisionFactura", factura.FechaEmisionFactura);
                    command.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    command.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                    command.Parameters.AddWithValue("@NumeroTotalArticulos", factura.NumeroTotalArticulos);
                    command.Parameters.AddWithValue("@SubTotalFacturas", factura.SubTotalFacturas);
                    command.Parameters.AddWithValue("@TotalImpuestos", factura.TotalImpuestos);
                    command.Parameters.AddWithValue("@TotalFactura", factura.TotalFactura);
                    connection.Open();
                    var facturaId = (int)command.ExecuteScalar();

                    var newFactura = GetById(facturaId);
                    return newFactura;
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

    public void Update(TblFacturas factura)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("UpdateFactura", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", factura.Id);
                    command.Parameters.AddWithValue("@FechaEmisionFactura", factura.FechaEmisionFactura);
                    command.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    command.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                    command.Parameters.AddWithValue("@NumeroTotalArticulos", factura.NumeroTotalArticulos);
                    command.Parameters.AddWithValue("@SubTotalFacturas", factura.SubTotalFacturas);
                    command.Parameters.AddWithValue("@TotalImpuestos", factura.TotalImpuestos);
                    command.Parameters.AddWithValue("@TotalFactura", factura.TotalFactura);
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
                using (SqlCommand command = new SqlCommand("DeleteFactura", connection))
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

    public IEnumerable<TblFacturas> GetFacturasByClienteId(int id)
    {
        List<TblFacturas> facturas = new List<TblFacturas>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetFacturasByClienteId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(new TblFacturas
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FechaEmisionFactura = Convert.ToDateTime(reader["FechaEmisionFactura"]),
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                NumeroFactura = Convert.ToInt32(reader["NumeroFactura"]),
                                NumeroTotalArticulos = Convert.ToInt32(reader["NumeroTotalArticulos"]),
                                SubTotalFacturas = Convert.ToDecimal(reader["SubTotalFacturas"]),
                                TotalImpuestos = Convert.ToDecimal(reader["TotalImpuestos"]),
                                TotalFactura = Convert.ToDecimal(reader["TotalFactura"])
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
        return facturas;
    }

    public IEnumerable<TblFacturas> GetFacturasByNumeroFactura(int numeroFactura)
    {
        List<TblFacturas> facturas = new List<TblFacturas>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetFacturasByNumeroFactura", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NumeroFactura", numeroFactura);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(new TblFacturas
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FechaEmisionFactura = Convert.ToDateTime(reader["FechaEmisionFactura"]),
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                NumeroFactura = Convert.ToInt32(reader["NumeroFactura"]),
                                NumeroTotalArticulos = Convert.ToInt32(reader["NumeroTotalArticulos"]),
                                SubTotalFacturas = Convert.ToDecimal(reader["SubTotalFacturas"]),
                                TotalImpuestos = Convert.ToDecimal(reader["TotalImpuestos"]),
                                TotalFactura = Convert.ToDecimal(reader["TotalFactura"])
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
        return facturas;
    }
}
