using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> FiltrarPorCodigo(string codigo);
        Task<List<Produto>> FiltrarPorDescricao(string descricao);
        Task<List<Produto>> FiltrarPorSetor(string setor);
        Task<Produto> DetalhesProduto(int id);        
        Task<Produto> InserirProduto(ProdutoViewModel produto);
        Task<Produto> AtualizarProduto(ProdutoViewModel produto);
        Task<bool> ExcluirProduto(int produtoId);
        Task<List<Fornecedor>> ObterFornecedor(int produtoId);
        Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId);
        Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao);
        Task<List<Setor>> ListarSetores();
    }
}
