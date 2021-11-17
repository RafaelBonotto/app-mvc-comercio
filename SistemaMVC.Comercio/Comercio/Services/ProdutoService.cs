﻿using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class ProdutoService : IProdutoService
    {
        public IRepositoryBase<Produto> _repositoryProduto;

        public ProdutoService(IRepositoryBase<Produto> context)
        {
            _repositoryProduto = context;
        }

        public Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> AtualizarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> DetalhesDoProduto(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExcluirProduto(int produtoId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> FiltrarProdutos(string codigo, string descricao, string setor)
        {
            try
            {
                var produto = new Produto()
                {
                    Codigo = codigo,
                    Descricao = descricao,
                    Setor = setor
                };
                return await _repositoryProduto.GetAllFilteredAsync(produto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Produto> InserirProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Produto>> ListarProdutos()
        {
            throw new NotImplementedException();
        }

        public Task<List<Fornecedor>> ObterFornecedor(int produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
