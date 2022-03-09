using System.Collections.Generic;
using System.Threading.Tasks;
using Comercio.Models;

namespace Comercio.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> FiltrarPorCodigo(string codigo);
        Task<List<Produto>> FiltrarPorDescricao(string descricao);
        Task<List<Produto>> FiltrarPorSetor(int setor_id);
        Task<Produto> DetalhesProduto(int id);        
        Task<Produto> InserirProduto(Produto produto);
        Task<Produto> AtualizarProduto(ProdutoViewModel produto);
        Task<bool> ExcluirProduto(int produtoId);
        Task<List<Fornecedor>> ObterFornecedor(int produtoId);
        Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId);
        Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao);

    }
}
