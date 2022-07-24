using Comercio.Entities;
using Comercio.Exceptions.Produto;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepositoryBase<Produto> _repositoryBase;
        private readonly IProdutoRepository _repository;
        private readonly IProdutoAdapter _mapper;

        public ProdutoService(IRepositoryBase<Produto> context, IProdutoRepository repository, IProdutoAdapter adapter)
        {
            _repositoryBase = context;
            _repository = repository;
            _mapper = adapter;
        }

        // Não implementado...
        public Task<Produto> AdicionarFornecedor(int produtoId, string fornecedorDescricao)
        {
            throw new NotImplementedException();
        }

        // Não implementado...
        public Task<List<Fornecedor>> ExcluirFornecedor(int produtoId, int fornecedorId)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Fornecedor>> ObterFornecedor(int produtoId)
            => await _repository.ObterFornecedor(produtoId);

        public async Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id)
            => await _repository.ObterFornecedorDetalhes(fornecedor_id);

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
                var checkProduto = await _repositoryBase.GetByKeyAsync(produto.Codigo);
                if (checkProduto.Any() && checkProduto.First().Ativo == 1)
                    throw new CodigoInvalidoException();
                if (checkProduto.Any() && checkProduto.First().Ativo == 0)
                {
                    produto.Id = checkProduto.First().Id;
                    return await this.AtualizarProduto(produto);
                }
                produto.Setor_id = await _repository.ObterSetorId(produto.SetorDescricao);
                var produtoRepository = _mapper.MontaProdutoInsertRepositorio(produto);
                return await _repositoryBase.AddAsync(produtoRepository);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Produto> InserirFornecedorProduto(int produtoId, string fornecedorDescricao)
            => await _repository.InserirFornecedorProduto(produtoId, fornecedorDescricao);

        public async Task<List<Setor>> ListarSetores
            ()=> await _repository.ObterSetores();
    }
}
