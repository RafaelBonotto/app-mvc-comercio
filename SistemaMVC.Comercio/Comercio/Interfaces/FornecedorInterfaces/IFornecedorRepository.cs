using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task<int> InserirTelefone(int fornecedor_id, List<Telefone> telefones);
        Task<int> InserirEndereco(int fornecedor_id, List<Endereco> telefones);
        Task<int> InserirVendedor(int fornecedor_id, List<Vendedor> telefones);
        Task<bool> InserirTelefoneFornecedor(List<FornecedorTelefone> fornecedorTelefone, MySqlConnection connection);
        Task<bool> InserirEnderecoFornecedor(List<FornecedorEndereco> fornecedorEndereco, MySqlConnection connection);
        Task<bool> InserirVendedorFornecedor(int fornecedor_id, List<Vendedor> vendedor, MySqlConnection connection);
    }
}
