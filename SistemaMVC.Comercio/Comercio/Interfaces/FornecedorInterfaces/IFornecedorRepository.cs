using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task InserirEndereco(int fornecedor_id, Endereco endereco);
        Task InserirVendedor(int fornecedor_id, Vendedor vendedor);
        Task<int> ObterIdTipoEndereco(string tipoEndereco);
        Task<List<TipoEnderecoResponse>> ObterTipoEndereco();
    }
}
