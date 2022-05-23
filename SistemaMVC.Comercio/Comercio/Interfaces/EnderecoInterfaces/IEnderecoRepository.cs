using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.EnderecoInterfaces
{
    public interface IEnderecoRepository
    {
        Task InserirEnderecoFornecedor(int fornecedor_id, Endereco endereco);
        Task<int> ObterIdTipoEndereco(string tipoEndereco);
        Task<List<TipoEnderecoResponse>> ObterDescricaoTipoEndereco();
        Task<List<Endereco>> ObterEnderecoDoFornecedor(int fornecedor_id);
    }
}
