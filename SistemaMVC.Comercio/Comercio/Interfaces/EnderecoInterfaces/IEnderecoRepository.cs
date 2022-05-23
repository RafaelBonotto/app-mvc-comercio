using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.EnderecoInterfaces
{
    public interface IEnderecoRepository
    {
        Task<bool> AtualizarEndereco(Endereco endereco);
        Task<bool> InserirEnderecoFornecedor(int fornecedor_id, Endereco endereco);
        Task<bool> ExcluirEnderecoFornecedor(int fornecedor_id, int endereco_id);
        Task<int> ObterIdTipoEndereco(string tipoEndereco);
        Task<List<TipoEnderecoResponse>> ObterDescricaoTipoEndereco();
        Task<List<Endereco>> ObterEnderecoFornecedor(int fornecedor_id);
    }
}