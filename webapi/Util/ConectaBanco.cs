using MySql.Data.MySqlClient;

namespace webapi.Util;


using MySql.Data.MySqlClient;

public class ConectaBanco
{
    private readonly MySqlConnection _connection;

    public ConectaBanco(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
    }

    public MySqlConnection GetConnection()
    {
        return _connection;
    }
}