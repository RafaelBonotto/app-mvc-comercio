using Comercio.Entities;
using Comercio.Models;
using Comercio.Responses.Produto;
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
        Task<List<FornecedorDescricaoId>> ObterFornecedorDescricaoId(int produtoId, MySqlConnection connection = null);
        Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id);
        Task<Produto> InserirFornecedorProduto(int produtoId, string fornecedorDescricao);
        Task<Produto> GetProdutoAsync(int produto_id, MySqlConnection connection);
        Task<List<FornecedorDescricaoId>> ExcluirFornecedor(int fornecedorId, int produtoId);
        Task<ObterFornecedoresEDadosDoProdutoResponse> ObterTodosFornecedoresEDadosDoProduto(int produtoId);
        Task<ObterProdutoListagemSetoresResponse> ObterProdutoListagemSetores(int produto_id);
    }
}
