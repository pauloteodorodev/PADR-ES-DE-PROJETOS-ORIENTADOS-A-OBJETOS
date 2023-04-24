using webapi.DTO;
using webapi.NewFolder;
using webapi.Util;

namespace webapi.Services
{
    public class ClienteService
    {
        private readonly ClienteDAO _clienteDAO;

        public ClienteService(ClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        public async Task<bool> Cadastrar(ClienteDTO clienteDTO)
        {
            var senhaSalt = CriptografiaSenha.CriarSalt();

            var cliente = new Cliente(
                    cpf: clienteDTO.CPF,
                    email: clienteDTO.Email,
                    senhaSalt: senhaSalt,
                    senha: CriptografiaSenha.CriptografarSenha(clienteDTO.Senha, senhaSalt),
                    ativo: true,
                    endereco: clienteDTO.Endereco,
                    telefone: clienteDTO.Telefone,
                    nome: clienteDTO.Nome
                );

            var resultado = await _clienteDAO.Cadastrar( cliente );

            return resultado;
        }


        public async Task<IEnumerable<Cliente>> ListarClientes()
        {

            var resultado = await _clienteDAO.ListarTodos();

            return resultado;
        }

        public async Task<bool> DeletarCliente(string id)
        {
            var resultado = await _clienteDAO.DeletarPorId(id);

            return resultado;
        }

        public async Task<Cliente?> BuscarPorId(string id)
        {
            var resultado = await _clienteDAO.BuscarPorId(id);

            return resultado;
        }
    }
}