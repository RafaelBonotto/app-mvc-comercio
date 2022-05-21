using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            await connection.QueryAsync(
                sql: TelefoneQuerys.DESATIVAR_TELEFONE_FORNECEDOR,
                param: new { fornecedor_id, telefone_id });
            return true;
        }

        public async Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone)
        {
            using var connection = await _connection.GetConnectionAsync();
            using var transaction = connection.BeginTransaction();
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

        public async Task<List<TipoTelefoneResponse>> ListarDescricaoTipoTelefone()
        {
            using var connection = await _connection.GetConnectionAsync();
            return (await connection.QueryAsync<TipoTelefoneResponse>(
                    TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();
        }

        public async Task<List<Telefone>> ListarTelefoneFornecedor(int fornecedor_id)
        {
            List<Telefone> ret = new();
            using var connection = await _connection.GetConnectionAsync();
            var telefoneIds = await connection.QueryAsync<int>(
                    sql: TelefoneQuerys.SELECT_ID_TELEFONE_FORNECEDOR,
                    param: new { fornecedor_id });
            if (telefoneIds.Any())
                foreach (var item in telefoneIds)
                    ret.Add(connection.Get<Telefone>(item));

            var tipoTelefoneDesc = (await connection.QueryAsync<TipoTelefoneResponse>(
                    TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();

            foreach (var telef in ret)
                telef.Tipo_telefone = tipoTelefoneDesc
                        .Where(x => x.Id == telef.Tipo_telefone_id)
                        .Select(x => x.Descricao).FirstOrDefault();
            return ret;
        }

        public async Task<int> ObterIdTipoTelefone(string tipoTelefone)
        {
            using var connection = await _connection.GetConnectionAsync();
            return connection.QueryFirstOrDefault<int>(
            TelefoneQuerys.SELECT_ID_TIPO_TELEFONE, new { tipoTelefone });
        }
    }
}
