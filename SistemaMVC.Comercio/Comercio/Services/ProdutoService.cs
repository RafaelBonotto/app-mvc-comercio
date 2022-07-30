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

        public async Task<List<Fornecedor>> ExcluirFornecedor(int fornecedorId, int produtoId)
        => await _repository.ExcluirFornecedor(fornecedorId, produtoId);

        public async Task<List<Fornecedor>> ObterFornecedor(int produtoId)
            => await _repository.ObterFornecedor(produtoId);

        public async Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id)
            => await _repository.ObterFornecedorDetalhes(fornecedor_id);

       

       
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
    }
}
