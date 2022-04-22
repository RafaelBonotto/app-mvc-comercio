using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Fornecedores
{
    public class FornecedorRepository : IRepositoryBase<Fornecedor>
    {
        private readonly IMySqlConnectionManager _connection;

        public FornecedorRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }

        public async Task<Fornecedor> AddAsync(Fornecedor fornecedor)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var row = await connection.InsertAsync<Fornecedor>(fornecedor);
                if (row > 0)
                    return await this.GetByIdAsync(row);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Fornecedor> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fornecedor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fornecedor>> GetByKeyAsync(string cnpj)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                List<Produto> ret = new();
                return (connection.Query<Fornecedor>(FornecedorQuerys.SELECT_POR_CNPJ, new { cnpj })).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }
    }
}
