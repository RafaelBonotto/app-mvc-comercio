using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task InserirTelefone(int fornecedor_id, List<Telefone> telefones, MySqlConnection connection);
        Task InserirEndereco(int fornecedor_id, List<Endereco> enderecos, MySqlConnection connection);
        Task InserirVendedor(int fornecedor_id, List<Vendedor> vendedores, MySqlConnection connection); 
    }
}
