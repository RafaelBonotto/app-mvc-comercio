using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Dapper;
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

        public async Task<Setor> AddAsync(Setor setor)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var setorId = await connection.InsertAsync<Setor>(setor);
                if (setorId > 0)
                    return await this.GetByIdAsync(setorId);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
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
                return (await connection.GetAllAsync<Setor>())
                            .Where(x => x.Ativo == 1)
                            .OrderBy(x => x.Descricao)
                            .ToList();
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

        public async Task<List<Setor>> GetByKeyAsync(string descricao)
        {
            try
            {
                List<Setor> ret = new();
                using var connction = await _connection.GetConnectionAsync();
                var response = await connction.QueryFirstOrDefaultAsync<Setor>(
                                SetorQuerys.SELECT_POR_DESCRICAO, new { descricao });
                if (response is null)
                    return ret;
                ret.Add(response);
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
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
