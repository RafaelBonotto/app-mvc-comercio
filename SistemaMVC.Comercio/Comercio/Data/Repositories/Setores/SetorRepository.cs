using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Setores
{
    public class SetorRepository : IRepositoryBase<Setor>
    {
        private readonly IMySqlConnectionManager _connection;

        public SetorRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }

        public Task<Setor> AddAsync(Setor entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Setor> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Setor>> GetAllAsync()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                //return (await connection.QueryAsync<Setor>(SetorQuerys.OBTER_SETORES)).ToList();
                return (await connection.GetAllAsync<Setor>()).ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Setor> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                //return await connection.QueryFirstOrDefaultAsync<Setor>(SetorQuerys.OBTER_SETORES, new { id });
                return await connection.GetAsync<Setor>(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<List<Setor>> GetByKeyAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Setor> UpdateAsync(Setor setor)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = await connection.UpdateAsync<Setor>(setor);
                if (!response)
                    return null;
                return await this.GetByIdAsync(setor.Id);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
