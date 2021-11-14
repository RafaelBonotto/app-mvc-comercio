﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Comercio.Models;

namespace Comercio.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> FiltrarProdutos(string codigo, string descricao, string setor);
        Task<Produto> DetalhesDoProduto(int id);        
        Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao);
        Task<Produto> InserirProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> ExcluirProduto(int produtoId);
    }
}
