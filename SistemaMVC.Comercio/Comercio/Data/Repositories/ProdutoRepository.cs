using Comercio.Data.Context;
using Comercio.Interfaces.Base;
using Comercio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories
{
    public class ProdutoRepository : IRepositoryBase<Produto>
    {
        private readonly ComercioDBContext _context;

        public ProdutoRepository(ComercioDBContext context)
        {
            _context = context;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    produto.Ativo = 1;
                    produto.Data_criacao = DateTime.Now;
                    produto.Data_alteracao = DateTime.Now;
                    produto.Data_fabricacao = DateTime.Now;
                    produto.Data_validade = DateTime.Now;
                    _context.TB_PRODUTO.Add(produto);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return produto;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var produto = _context.TB_PRODUTO.Find(id);
                    produto.Ativo = 0;
                    produto.Data_alteracao = DateTime.Now;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return produto;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            try
            {               
                return await _context.TB_PRODUTO.Where(x => x.Ativo == 1).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> GetAllFilteredAsync(Produto produto)
        {
            try
            {
                var produtosBanco = new List<Produto>();

                if (!string.IsNullOrEmpty(produto.Codigo))// Caso recebe código filtra apenas pelo código
                {
                    return await _context.TB_PRODUTO.Where(x => x.Codigo.Equals(produto.Codigo)).ToListAsync();
                }

                if (!string.IsNullOrEmpty(produto.Descricao))
                {
                    produtosBanco = await _context.TB_PRODUTO.Where(x => x.Descricao.Replace(" ", "").ToUpper().Contains(produto.Descricao.Replace(" ", "").ToUpper())).ToListAsync();
                }
                
                if (string.IsNullOrEmpty(produto.Setor))
                {
                    produtosBanco = produtosBanco.Where(x => x.Setor.Equals(produto.Setor)).ToList();
                }

                return produtosBanco;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            try
            {
                return await _context.TB_PRODUTO.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> GetByKeyAsync(string key)
        {
            try
            {
                return await _context.TB_PRODUTO.Where(x => x.Descricao == key).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var produtoBanco = await _context.TB_PRODUTO.FindAsync(produto.Id);
                    produtoBanco.Ativo = 1;
                    produtoBanco.Data_alteracao = DateTime.Now;
                    produtoBanco.Descricao = produto.Descricao;
                    produtoBanco.Preco_custo = produto.Preco_custo;
                    produtoBanco.Preco_venda = produto.Preco_venda;
                    _context.TB_PRODUTO.Update(produtoBanco);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return produto;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
