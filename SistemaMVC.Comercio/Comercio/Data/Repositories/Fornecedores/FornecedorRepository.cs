using Comercio.Entities;
using Comercio.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Fornecedores
{
    public class FornecedorRepository : IRepositoryBase<Fornecedor>
    {
        public Task<Fornecedor> AddAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
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

        public Task<List<Fornecedor>> GetByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }
    }
}
