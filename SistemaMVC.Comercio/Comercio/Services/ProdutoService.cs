using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Comercio.Mapper;

namespace Comercio.Services
{
    public class ProdutoService : IProdutoService
    {
        public IRepositoryBase<Produto> _repositoryBase;
        public IProdutoRepository _repository;

        public ProdutoService(IRepositoryBase<Produto> context, IProdutoRepository repository)
        {
            _repositoryBase = context;
            _repository = repository;
        }

        public Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            try
            {
                Produto produtoRepository = await _repositoryBase.GetByIdAsync(produto.Id);
                produtoRepository = Adapter.MontaProdutoUpdateRepositorio(
                                                            produtoRequest: produto, 
                                                            produtoRepositorio: produtoRepository);          
                return await _repositoryBase.UpdateAsync(produtoRepository);
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
                return _repositoryBase.GetByIdAsync(id);
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
                Produto response = await _repositoryBase.DeleteAsync(produtoId);
                if (response is null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorCodigo(string codigo)
        {
            try
            {
                return await _repositoryBase.GetByKeyAsync(codigo);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorDescricao(string descricao)
        {
            try
            {
                return await _repository.FiltrarPorDescricao(descricao);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorSetor(int setor_id)
        {
            try
            {
                return await _repository.FiltrarPorSetor(setor_id);
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
