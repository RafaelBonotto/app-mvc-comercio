using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Models;
using Dapper;
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

        public Task<Produto> AddAsync(Produto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> FiltrarPorDescricao(string descricao)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto>(ProdutoQuery.SELECT_POR_DESCRICAO, param: new { descricao }).ToList();
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
                var response = connection.Query<Produto>(ProdutoQuery.SELECT_POR_SETOR_ID, param:  new { setor_id }).ToList();
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

        public async Task<Produto> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.QueryFirstOrDefault<Produto>(ProdutoQuery.SELECT_POR_ID, param:  new { id });
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
                var response = connection.Query<Produto>(ProdutoQuery.SELECT_POR_CODIGO, param: new { codigo }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Produto> UpdateAsync(Produto entity)
        {
            throw new NotImplementedException();
        }
    }
}
