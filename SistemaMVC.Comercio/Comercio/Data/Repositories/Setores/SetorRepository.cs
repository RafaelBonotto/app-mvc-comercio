using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Dapper.Contrib.Extensions;
using System;
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

        public async Task<Setor> DeleteAsync(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var setor = await this.GetByIdAsync(id);
            setor.Ativo = 0;
            setor.Data_alteracao = DateTime.Now;
            var response = connection.Update<Setor>(setor);
            if (!response)
                return null;
            return setor;
        }

        public async Task<List<Setor>> GetAllAsync()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                return (await connection.GetAllAsync<Setor>()).Where(x => x.Ativo == 1).ToList();
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
