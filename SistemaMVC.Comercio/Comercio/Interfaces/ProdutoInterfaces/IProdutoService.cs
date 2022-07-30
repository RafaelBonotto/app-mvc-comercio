using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoService
    {
        Task<Produto> AtualizarProduto(ProdutoViewModel produto);
        Task<bool> ExcluirProduto(int produtoId);
        Task<List<Fornecedor>> ObterFornecedor(int produtoId);
        Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id);
        Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId);
        Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao);
    }
}
