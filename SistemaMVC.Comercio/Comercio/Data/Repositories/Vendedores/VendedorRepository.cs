using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.VendedorInterfaces;
using Dapper.Contrib.Extensions;
using System;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Vendedores
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IVendedorAdapter _mapperVendedor;

        public VendedorRepository(
            IMySqlConnectionManager connection, 
            IVendedorAdapter mapperVendedor)
        {
            _connection = connection;
            _mapperVendedor = mapperVendedor;
        }

        public async Task<bool> InserirVendedorFornecedor(int fornecedor_id, Pessoa pessoa)
        {
            using var connection = await _connection.GetConnectionAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var pessoa_id = await connection.InsertAsync<Pessoa>(pessoa, transaction);
                if (pessoa_id <= 0)
                    return false;

                var row = await connection.InsertAsync<VendedorFornecedor>(
                    entityToInsert: _mapperVendedor.MontaVendedorFornecedor(fornecedor_id, pessoa_id), transaction);
                if (row <= 0)
                {
                    transaction.Rollback();
                    return false;
                }
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
