using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories
{
    public class ProdutoRepository : IRepositoryBase<Produto>, IProdutoRepository
    {
        private readonly IMySqlConnectionManager _connection;

        public ProdutoRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = await connection.InsertAsync<Produto>(produto);
                produto.Id = response;
                return produto;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produto = await this.GetByIdAsync(id);
            produto.Ativo = 0;
            produto.Data_alteracao = DateTime.Now;
            var response = connection.Update<Produto>(produto);
            if (!response) 
                return null;
            return produto;
        }

        public async Task<List<Produto>> FiltrarPorDescricao(string descricao)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                    sql: ProdutoQuery.SELECT_POR_DESCRICAO, 
                    (produto, setor) =>
                    {
                        produto.Setor = setor;
                        return produto;
                    },
                    param: new { descricao }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorSetor(int setor_id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                                    sql: ProdutoQuery.SELECT_POR_SETOR_ID,
                                    (produto, setor) =>
                                    {
                                        produto.Setor = setor;
                                        return produto;
                                    },
                                    param: new { setor_id }).ToList(); 
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<Produto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> GetByIdAsync(int produto_id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                                   sql: ProdutoQuery.SELECT_POR_ID,
                                   (produto, setor) =>
                                   {
                                       produto.Setor = setor;
                                       return produto;
                                   },
                                   param: new { produto_id }).FirstOrDefault();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> GetByKeyAsync(string codigo)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                                                    sql: ProdutoQuery.SELECT_POR_CODIGO,
                                                    (produto, setor) =>
                                                    {
                                                        produto.Setor = setor;
                                                        return produto;
                                                    },
                                                    param: new { codigo }).ToList(); 
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            try
            {                
                using var connection = await _connection.GetConnectionAsync();
                var response = await connection.UpdateAsync<Produto>(produto);
                if (!response)
                    return null;
                return produto;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
