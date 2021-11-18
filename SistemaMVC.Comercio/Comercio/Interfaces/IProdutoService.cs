using System.Collections.Generic;
using System.Threading.Tasks;
using Comercio.Models;

namespace Comercio.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> FiltrarProdutos(string codigo, string descricao, string setor);
        Task<Produto> DetalhesProduto(int id);        
        Task<Produto> InserirProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> ExcluirProduto(int produtoId);
        Task<List<Fornecedor>> ObterFornecedor(int produtoId);
        Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId);
        Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao);

    }
}
