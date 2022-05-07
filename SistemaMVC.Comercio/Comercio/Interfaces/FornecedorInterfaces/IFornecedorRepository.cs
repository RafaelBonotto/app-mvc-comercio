using Comercio.Entities;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task InserirTelefone(int fornecedor_id, Telefone telefone); 
        Task InserirEndereco(int fornecedor_id, Endereco endereco);
        Task InserirVendedor(int fornecedor_id, Vendedor vendedor);
        Task<int> ObterIdTipoTelefone(string tipoTelefone);
    }
}
