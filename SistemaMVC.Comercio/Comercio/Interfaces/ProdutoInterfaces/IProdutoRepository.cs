using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> FiltrarPorDescricao(string descricao);
        Task<List<Produto>> FiltrarPorSetor(string setor);
        Task<List<Setor>> ObterSetores();
        Task<int> ObterSetorId(string setor);
        Task<List<Fornecedor>> ObterFornecedor(int produto_id, MySqlConnection connection = null);
        Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id);
        Task<Produto> InserirFornecedorProduto(int produtoId, string fornecedorDescricao);
        Task<Produto> GetProdutoAsync(int produto_id, MySqlConnection connection);
        Task<List<Fornecedor>> ExcluirFornecedor(int fornecedorId, int produtoId);
    }
}
