using Comercio.Interfaces;
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

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            try
            {
                return await _repositoryProduto.UpdateAsync(produto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Produto> DetalhesProduto(int id)
        {
            try
            {
                return _repositoryProduto.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExcluirProduto(int produtoId)
        {
            try
            {
                var produto = await _repositoryProduto.DeleteAsync(produtoId);

                if (produto.Id == produtoId) 
                    return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarProdutoPorCodigo(string codigo)
        {
            // ALTERADO ...
            try
            {
                return await _repositoryProduto.GetByKeyAsync(codigo);
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
