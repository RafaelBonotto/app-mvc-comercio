using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Requests.Fornecedor;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.EnderecoInterfaces
{
    public interface IEnderecoRepository 
    {
        Task<bool> AtualizarEndereco(EnderecoRequest endereco, MySqlConnection connection = null);
        Task<Endereco> GetById(int id);
        Task<bool> InserirEnderecoFornecedor(int fornecedor_id, Endereco endereco, MySqlConnection connection = null);
        Task<bool> ExcluirEnderecoFornecedor(int fornecedor_id, int endereco_id, MySqlConnection connection = null);
        Task<int> ObterIdTipoEndereco(string tipoEndereco, MySqlConnection connection = null);
        Task<List<TipoEnderecoResponse>> ObterDescricaoTipoEndereco(MySqlConnection connection = null);
        Task<List<Endereco>> ObterEnderecoFornecedor(int fornecedor_id, MySqlConnection conn = null);
    }
}