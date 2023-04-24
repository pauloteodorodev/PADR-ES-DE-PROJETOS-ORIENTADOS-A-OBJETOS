using MySql.Data.MySqlClient;
using webapi.DTO;
using webapi.NewFolder;

namespace webapi
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly MySqlConnection _connection;

        public ClienteDAO(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Cliente?> BuscarPorId(string id)
        {
            await _connection.OpenAsync();

            string query = "SELECT Id,Nome, CPF, Email, SenhaSalt, Senha, Telefone, Ativo, Endereco FROM umc.Clientes WHERE Id = @Id LIMIT 1";
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Cliente cliente = new Cliente(
                            reader.GetString("CPF"),
                            reader.GetString("Email"),
                            reader.GetString("SenhaSalt"),
                            reader.GetString("Senha"),
                            reader.GetBoolean("Ativo"),
                            reader.GetString("Endereco"),
                            reader.GetString("Telefone"),
                            reader.GetString("Nome")
                        );
                        cliente.Id = reader.GetGuid("Id");
                        _connection.Close();
                        return cliente;
                    }
                }
            }
            _connection.Close();

            return null;
        }

        public async Task<bool> Cadastrar(Cliente cliente)
        {
            bool success = false;
            await _connection.OpenAsync();
            using (MySqlCommand command = new MySqlCommand("INSERT INTO umc.Clientes (Id, Nome, CPF, Email, SenhaSalt, Senha, Telefone, Ativo, Endereco) VALUES (@Id, @Nome, @CPF, @Email, @SenhaSalt, @Senha, @Telefone, @Ativo, @Endereco)", _connection))
            {
                command.Parameters.AddWithValue("@Id", cliente.Id);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@CPF", cliente.CPF);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@SenhaSalt", cliente.SenhaSalt);
                command.Parameters.AddWithValue("@Senha", cliente.Senha);
                command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                command.Parameters.AddWithValue("@Ativo", cliente.Ativo);
                command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                success = await command.ExecuteNonQueryAsync() == 1;
            }
            _connection.Close();
            return success;
        }

        public async Task<bool> DeletarPorId(string id)
        {
            bool success = false;
            await _connection.OpenAsync();
            using (MySqlCommand command = new MySqlCommand("DELETE FROM umc.Clientes WHERE Id = @Id", _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                success = await command.ExecuteNonQueryAsync() == 1;
            }
            _connection.Close();
            return success;
        }

       

        public async Task<List<Cliente>> ListarTodos()
        {
            List<Cliente> clientes = new List<Cliente>();

            await _connection.OpenAsync();

            string query = "SELECT Id,Nome, CPF, Email, SenhaSalt, Senha, Telefone, Ativo, Endereco FROM umc.Clientes";
            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Cliente cliente = new Cliente(
                            reader.GetString("CPF"),
                            reader.GetString("Email"),
                            reader.GetString("SenhaSalt"),
                            reader.GetString("Senha"),
                            reader.GetBoolean("Ativo"),
                            reader.GetString("Endereco"),
                            reader.GetString("Telefone"),
                            reader.GetString("Nome")
                        );
                        cliente.Id = reader.GetGuid("Id");
                        clientes.Add(cliente);
                    }
                }
            }

            _connection.Close();

            return clientes;
        }

    }
}