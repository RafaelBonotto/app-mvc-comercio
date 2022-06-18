using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.EnderecoInterfaces
{
    public interface IEnderecoRepository 
    {
        Task<bool> AtualizarEndereco(Endereco endereco);
        Task<Endereco> GetById(int id);
        Task<bool> InserirEnderecoFornecedor(int fornecedor_id, Endereco endereco);
        Task<bool> ExcluirEnderecoFornecedor(int fornecedor_id, int endereco_id);
        Task<int> ObterIdTipoEndereco(string tipoEndereco);
        Task<List<TipoEnderecoResponse>> ObterDescricaoTipoEndereco(MySqlConnection connection = null);
        Task<List<Endereco>> ObterEnderecoFornecedor(int fornecedor_id, MySqlConnection conn = null);
    }
}