using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Mapper;
using Comercio.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepositoryBase<Produto> _repositoryBase;
        private readonly IProdutoRepository _repository;
        private readonly IAdapter _mapper;

        public ProdutoService(IRepositoryBase<Produto> context, IProdutoRepository repository, IAdapter adapter)
        {
            _repositoryBase = context;
            _repository = repository;
            _mapper = adapter;
        }

        public Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> AdicionarProduto(ProdutoViewModel produto)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> AtualizarProduto(ProdutoViewModel produto)
        {
            try
            {
                Produto produtoRepository = await _repositoryBase.GetByIdAsync(produto.Id);
                produto.Setor_id = await _repository.ObterSetorId(produto.SetorDescricao);
                produtoRepository = _mapper.MontaProdutoUpdateRepositorio(
                                                            produtoViewModel: produto,
                                                            produtoRepositorio: produtoRepository);
                return await _repositoryBase.UpdateAsync(produtoRepository);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Produto> DetalhesProduto(int id)
        {
            try
            {
                return await _repositoryBase.GetByIdAsync(id);                 
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

        public async Task<List<Produto>> FiltrarPorSetor(string setor)
        {
            try
            {
                return await _repository.FiltrarPorSetor(setor);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Produto> InserirProduto(ProdutoViewModel produto)
        {
            try
            {
                produto.Setor_id = await _repository.ObterSetorId(produto.SetorDescricao);
                var produtoRepository = _mapper.MontaProdutoInsertRepositorio(produto);
                return await _repositoryBase.AddAsync(produtoRepository);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<List<Produto>> ListarProdutos()
        {
            throw new NotImplementedException();
        }

        public Task<List<Fornecedor>> ObterFornecedor(int produtoId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Setor>> ListarSetores
            ()=> await _repository.ObterSetores();
    }
}
