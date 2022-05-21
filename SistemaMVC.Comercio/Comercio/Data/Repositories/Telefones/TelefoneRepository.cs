using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Telefones
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly ITelefoneAdapter _mapperTelefone;

        public TelefoneRepository(
            IMySqlConnectionManager connection,
            ITelefoneAdapter mapperTelefone)
        {
            _connection = connection;
            _mapperTelefone = mapperTelefone;
        }

        public bool EditarTelefone(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone)
        {
            using (var connection = await _connection.GetConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var telefone_id = await connection.InsertAsync<Telefone>(telefone, transaction);
                        if (telefone_id <= 0)
                            throw new Exception("Erro ao tentar inserir o telefone do fornecedor");

                        var row = await connection.InsertAsync<TelefoneFornecedor>(
                            entityToInsert: _mapperTelefone.MontaTelefoneFornecedor(fornecedor_id, telefone_id), transaction);
                        if (row <= 0)
                        {
                            transaction.Rollback();
                            throw new Exception("Erro ao tentar inserir o telefone do fornecedor");
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

        public List<string> ListarDescricaoTipoTelefone()
        {
            throw new System.NotImplementedException();
        }

        public List<Telefone> ListarTelefoneFornecedor(int fornecedor_id)
        {
            throw new System.NotImplementedException();
        }
    }
}
