using Comercio.Data.ConnectionManager;
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
            var sql = $"SELECT * FROM tb_produto WHERE descricao LIKE '%{descricao}%'";
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto>(sql).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorsetor(int setor_id)
        {
            var sql = $"SELECT * FROM tb_produto WHERE setor_id = {setor_id}";
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto>(sql).ToList();
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

        public Task<Produto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> GetByKeyAsync(string key)
        {
            var sql = $"SELECT * FROM tb_produto WHERE codigo = {key}";
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto>(sql).ToList();
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
