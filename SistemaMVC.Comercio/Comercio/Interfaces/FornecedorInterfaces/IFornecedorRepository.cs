using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task<bool> InserirTelefoneFornecedor(List<FornecedorTelefone> fornecedorTelefone, MySqlConnection connection);
        Task<bool> InserirEnderecoFornecedor(List<FornecedorEndereco> fornecedorEndereco, MySqlConnection connection);
        Task<bool> InserirVendedorFornecedor(int fornecedor_id, List<Vendedor> vendedor, MySqlConnection connection);
    }
}
