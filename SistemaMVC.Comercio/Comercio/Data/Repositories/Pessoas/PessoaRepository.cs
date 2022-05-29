using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Pessoas
{
    public class PessoaRepository : IRepositoryBase<Pessoa>
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IPessoaAdapter _mapperPessoa;

        public PessoaRepository(
            IMySqlConnectionManager connection, 
            IPessoaAdapter mapperPessoa)
        {
            _connection = connection;
            _mapperPessoa = mapperPessoa;
        }

        public Task<Pessoa> AddAsync(Pessoa entity)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pessoa>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pessoa>> GetByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> UpdateAsync(Pessoa entity)
        {
            throw new NotImplementedException();
        }
    }
}
