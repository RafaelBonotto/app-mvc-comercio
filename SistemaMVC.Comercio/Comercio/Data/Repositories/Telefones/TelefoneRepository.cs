using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Requests.Fornecedor;
using Dapper;
using Dapper.Contrib.Extensions;
using MySqlConnector;
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

        public async Task<bool> AtualizarTelefone(TelefoneRequest telefone, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                var telefoneBanco = conn.Get<Telefone>(telefone.Telefone_id);
                if (telefoneBanco is null)
                    return false;

                telefoneBanco.Ddd = telefone.Ddd;
                telefoneBanco.Numero = telefone.Numero;
                telefoneBanco.Tipo_telefone_id = telefone.Tipo_telefone_id;
                telefoneBanco.Ativo = 1;
                telefoneBanco.Data_alteracao = DateTime.Now;
                return connection.Update<Telefone>(telefoneBanco);
            }
            else
            {
                var telefoneBanco = connection.Get<Telefone>(telefone.Telefone_id);
                if (telefoneBanco is null)
                    return false;

                telefoneBanco.Ddd = telefone.Ddd;
                telefoneBanco.Numero = telefone.Numero;
                telefoneBanco.Tipo_telefone_id = telefone.Tipo_telefone_id;
                telefoneBanco.Ativo = 1;
                telefoneBanco.Data_alteracao = DateTime.Now;
                return connection.Update<Telefone>(telefoneBanco);
            }
        }

        public async Task<bool> ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            await connection.QueryAsync(
                sql: TelefoneQuerys.DESATIVAR_TELEFONE_FORNECEDOR,
                param: new { fornecedor_id, telefone_id });
            return true;
        }

        public async Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone, MySqlConnection conn = null)
        {
            if(conn is null)
            {
                using var connection = await _connection.GetConnectionAsync();
                using var transaction = connection.BeginTransaction();
                try
                {
                    var telefone_id = await connection.InsertAsync<Telefone>(telefone, transaction);
                    if (telefone_id <= 0)
                        return false;

                    var row = await connection.InsertAsync<TelefoneFornecedor>(
                        entityToInsert: _mapperTelefone.MontaTelefoneFornecedor(fornecedor_id, telefone_id),
                        transaction: transaction);

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
            else
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    var telefone_id = await conn.InsertAsync<Telefone>(telefone, transaction);
                    if (telefone_id <= 0)
                        return false;

                    var row = await conn.InsertAsync<TelefoneFornecedor>(
                        entityToInsert: _mapperTelefone.MontaTelefoneFornecedor(fornecedor_id, telefone_id),
                        transaction: transaction);

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

        public async Task<List<TipoTelefoneResponse>> ListarDescricaoTipoTelefone(MySqlConnection connection = null)
        {
            if(connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                return (await conn.QueryAsync<TipoTelefoneResponse>(
                    TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();
            }
            else
            {
                return (await connection.QueryAsync<TipoTelefoneResponse>(
                   TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();
            }
        }
            

        public async Task<List<Telefone>> ListarTelefoneFornecedor(int fornecedor_id, MySqlConnection conn = null)
        {
            List<Telefone> ret = new();
            List<TipoTelefoneResponse> tiposTel = new();
            if (conn is null)
            {
                using var connection = await _connection.GetConnectionAsync();
                var telefoneIds = await connection.QueryAsync<int>(
                    sql: TelefoneQuerys.SELECT_ID_TELEFONE_FORNECEDOR,
                    param: new { fornecedor_id });

                if (telefoneIds.Any())
                    foreach (var id in telefoneIds)
                        ret.Add(connection.Get<Telefone>(id));

                tiposTel = (await connection.QueryAsync<TipoTelefoneResponse>(
                        TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();
            }
            else
            {
                var telefoneIds = await conn.QueryAsync<int>(
                    sql: TelefoneQuerys.SELECT_ID_TELEFONE_FORNECEDOR,
                    param: new { fornecedor_id });

                if (telefoneIds.Any())
                    foreach (var id in telefoneIds)
                        ret.Add(conn.Get<Telefone>(id));

                tiposTel = (await conn.QueryAsync<TipoTelefoneResponse>(
                        TelefoneQuerys.SELECT_TIPO_TELEFONE)).ToList();
            }
            foreach (var telef in ret)
                telef.Tipo_telefone = tiposTel
                        .Where(x => x.Id == telef.Tipo_telefone_id)
                        .Select(x => x.Descricao).FirstOrDefault();
            return ret;
        }

        public async Task<int> ObterIdTipoTelefone(string tipoTelefone, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                return conn.QueryFirstOrDefault<int>(
                            sql: TelefoneQuerys.SELECT_ID_TIPO_TELEFONE,
                            param: new { tipoTelefone });
            }
            return connection.QueryFirstOrDefault<int>(
                        sql: TelefoneQuerys.SELECT_ID_TIPO_TELEFONE, 
                        param: new { tipoTelefone });
        }

        public async Task<Telefone> GetById(int telefone_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var telefoneBanco = connection.Get<Telefone>(telefone_id);
            if (telefoneBanco is null)
                return null;
            telefoneBanco.TiposTelefone = await this.ListarDescricaoTipoTelefone(connection);
            return telefoneBanco;
        }
    }
}
