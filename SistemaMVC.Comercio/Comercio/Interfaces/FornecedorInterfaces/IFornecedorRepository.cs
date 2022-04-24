using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task<int> InserirTelefone(int fornecedor_id, List<Telefone> telefones, MySqlConnection connection);
        Task<int> InserirEndereco(int fornecedor_id, List<Endereco> enderecos, MySqlConnection connection);
        Task<int> InserirVendedor(int fornecedor_id, List<Vendedor> vendedores, MySqlConnection connection); 
        //Task<bool> InserirTelefoneFornecedor(List<FornecedorTelefone> fornecedorTelefone, MySqlConnection connection);
        //Task<bool> InserirEnderecoFornecedor(List<FornecedorEndereco> fornecedorEndereco, MySqlConnection connection);
        //Task<bool> InserirVendedorFornecedor(int fornecedor_id, List<Vendedor> vendedor, MySqlConnection connection);
    }
}
