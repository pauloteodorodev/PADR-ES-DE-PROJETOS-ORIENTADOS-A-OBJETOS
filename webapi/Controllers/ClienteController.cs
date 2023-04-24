using Microsoft.AspNetCore.Mvc;
using System.Net;
using webapi.DTO;
using webapi.NewFolder;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpPost("cadastrar")]
        public async Task<ActionResult<bool>> Cadastrar([FromServices] ClienteDAO clienteDAO, [FromBody] ClienteDTO clienteDTO)
        {
            ClienteService cadastrarClienteService = new(clienteDAO);
            var resultado = await cadastrarClienteService.Cadastrar(clienteDTO);

            return resultado;
        }

        [HttpGet("listar-todos")]
        public async Task<List<ClienteDTO>> ListarTodos([FromServices] ClienteDAO clienteDAO)
        {
            ClienteService cadastrarClienteService = new ClienteService(clienteDAO);
            var resultado = await cadastrarClienteService.ListarClientes();

            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in resultado)
            {
                var clienteDTO = new ClienteDTO
                {
                    Id = cliente.Id.ToString(),
                    CPF = cliente.CPF,
                    Nome = cliente.Nome,
                    
                };
                clientesDTO.Add(clienteDTO);
            }

            return clientesDTO;
        }

        [HttpDelete("deletar/{id}")]
        public async Task<bool> DeletarClite(string id, [FromServices] ClienteDAO clienteDAO)
        {
            ClienteService clienteService = new ClienteService(clienteDAO);
            var resultado = await clienteService.DeletarCliente(id);

            return resultado;
        }


        [HttpGet("buscar-cliente")]
        public async Task<ClienteDTO?> BuscarCliente([FromServices] ClienteDAO clienteDAO,string id)
        {
            ClienteService cadastrarClienteService = new ClienteService(clienteDAO);
            var cliente = await cadastrarClienteService.BuscarPorId(id);          

           if(cliente is not null)
           {
                var clienteDTO = new ClienteDTO
                {
                    Id = cliente.Id.ToString(),
                    CPF = cliente.CPF,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Endereco = cliente.Endereco,
                    Telefone = cliente.Telefone,
                    Senha = cliente.Senha,
                };

                return clienteDTO;
           }

            return null ;
        }


    }
}


