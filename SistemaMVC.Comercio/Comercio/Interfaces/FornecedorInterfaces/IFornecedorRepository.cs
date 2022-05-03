using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task InserirTelefone(int fornecedor_id, List<Telefone> telefones); 
        Task InserirEndereco(int fornecedor_id, List<Endereco> enderecos);
        Task InserirVendedor(int fornecedor_id, List<Vendedor> vendedores); 
    }
}
