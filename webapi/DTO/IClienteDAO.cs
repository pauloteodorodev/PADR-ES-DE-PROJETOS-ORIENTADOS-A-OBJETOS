using webapi.NewFolder;

namespace webapi.DTO
{
    public interface IClienteDAO
    {
        public Task<List<Cliente>> ListarTodos();
        public Task<bool> Cadastrar(Cliente cliente);
        public Task<Cliente?> BuscarPorId(string id);
        public Task<bool> DeletarPorId(string id);
    }
}
