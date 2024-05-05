using System.Data.SqlClient;
using WebApi.Models;

public class CatTipoClienteRepository : ICatTipoClienteRepository
{
    //private readonly SqlConnection _connection;

    //public CatTipoClienteRepository(SqlConnection connection)
    //{
    //    _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    //}

    //public IEnumerable<CatTipoCliente> GetAll()
    //{
    //    List<CatTipoCliente> tiposCliente = new List<CatTipoCliente>();

    //    string query = "SELECT * FROM CatTipoCliente";

    //    try
    //    {
    //        using (SqlCommand command = new SqlCommand(query, _connection))
    //        {
    //            _connection.Open();
    //            using (SqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    tiposCliente.Add(new CatTipoCliente
    //                    {
    //                        Id = Convert.ToInt32(reader["Id"]),
    //                        TipoCliente = reader["TipoCliente"].ToString()
    //                    });
    //                }
    //            }
    //        }

    //        return tiposCliente;
    //    }
    //    finally
    //    {
    //        if (_connection != null)
    //        {
    //            _connection.Close();
    //        }
    //    }
    //}

    //public CatTipoCliente GetById(int id)
    //{
    //    CatTipoCliente tipoCliente = null;

    //    try
    //    {
    //        using (SqlCommand command = new SqlCommand("SELECT * FROM CatTipoCliente WHERE Id = @Id", _connection))
    //        {
    //            command.Parameters.AddWithValue("@Id", id);
    //            _connection.Open();
    //            SqlDataReader reader = command.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                tipoCliente = new CatTipoCliente
    //                {
    //                    Id = Convert.ToInt32(reader["Id"]),
    //                    TipoCliente = reader["TipoCliente"].ToString()
    //                };
    //            }
    //            reader.Close();
    //        }

    //        return tipoCliente;
    //    }
    //    finally
    //    {
    //        if (_connection != null)
    //        {
    //            _connection.Close();
    //        }
    //    }
    //}

    //public void Add(CatTipoCliente tipoCliente)
    //{
    //    try
    //    {
    //        using (SqlCommand command = new SqlCommand("INSERT INTO CatTipoCliente (TipoCliente) VALUES (@TipoCliente)", _connection))
    //        {
    //            command.Parameters.AddWithValue("@TipoCliente", tipoCliente.TipoCliente);

    //            _connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //    finally
    //    {
    //        if (_connection != null)
    //        {
    //            _connection.Close();
    //        }
    //    }
    //}

    //public void Update(CatTipoCliente tipoCliente)
    //{
    //    try
    //    {
    //        using (SqlCommand command = new SqlCommand("UPDATE CatTipoCliente SET TipoCliente = @TipoCliente WHERE Id = @Id", _connection))
    //        {
    //            command.Parameters.AddWithValue("@Id", tipoCliente.Id);
    //            command.Parameters.AddWithValue("@TipoCliente", tipoCliente.TipoCliente);

    //            _connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //    finally
    //    {
    //        if (_connection != null)
    //        {
    //            _connection.Close();
    //        }
    //    }
    //}

    //public void Delete(int id)
    //{
    //    try
    //    {
    //        using (SqlCommand command = new SqlCommand("DELETE FROM CatTipoCliente WHERE Id = @Id", _connection))
    //        {
    //            command.Parameters.AddWithValue("@Id", id);
    //            _connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //    finally
    //    {
    //        if (_connection != null)
    //        {
    //            _connection.Close();
    //        }
    //    }
    //}
}
