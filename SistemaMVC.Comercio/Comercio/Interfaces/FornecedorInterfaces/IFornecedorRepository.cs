using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task InserirTelefone(int fornecedor_id, Telefone telefone); 
        Task ExcluirTelefone(int fornecedor_id, int telefone_id); 
        Task InserirEndereco(int fornecedor_id, Endereco endereco);
        Task InserirVendedor(int fornecedor_id, Vendedor vendedor);
        Task<int> ObterIdTipoTelefone(string tipoTelefone);
        Task<int> ObterIdTipoEndereco(string tipoEndereco);
        Task<List<TipoTelefoneResponse>> ObterDescricaoTipoTelefone();
        Task<List<TipoEnderecoResponse>> ObterTipoEndereco();
    }
}
