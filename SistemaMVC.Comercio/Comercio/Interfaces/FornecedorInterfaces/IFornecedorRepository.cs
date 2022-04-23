using Comercio.Entities;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository 
    {
        Task<int> InserirTelefoneFornecedor(FornecedorTelefone fornecedorTelefone);
        Task<int> InserirEnderecoFornecedor(FornecedorEndereco fornecedorEndereco);
        Task<int> InserirVendedorFornecedor(int fornecedor_id, Vendedor vendedor);
    }
}
