using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Requests.Fornecedor;
using Dapper;
using Dapper.Contrib.Extensions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Enderecos
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IEnderecoAdapter _mapper;

        public EnderecoRepository(
            IMySqlConnectionManager connection, 
            IEnderecoAdapter mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<bool> InserirEnderecoFornecedor(int fornecedor_id, Endereco endereco, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using (var conn = await _connection.GetConnectionAsync())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            var endereco_id = await conn.InsertAsync<Endereco>(endereco, transaction);
                            if (endereco_id <= 0)
                                return false;

                            var row = await conn.InsertAsync<EnderecoFornecedor>(
                                _mapper.MontaInsertEnderecoFornecedor(fornecedor_id, endereco_id), transaction);
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
            else
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var endereco_id = await connection.InsertAsync<Endereco>(endereco, transaction);
                        if (endereco_id <= 0)
                            return false;

                        var row = await connection.InsertAsync<EnderecoFornecedor>(
                            _mapper.MontaInsertEnderecoFornecedor(fornecedor_id, endereco_id), transaction);
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

        public async Task<int> ObterIdTipoEndereco(string tipoEndereco, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                return conn.QueryFirstOrDefault<int>(
                                sql: EnderecoQuerys.SELECT_ID_TIPO_ENDERECO, 
                                param: new { tipoEndereco });
            }
            else
            {
                return connection.QueryFirstOrDefault<int>(
                                sql: EnderecoQuerys.SELECT_ID_TIPO_ENDERECO,
                                param: new { tipoEndereco });
            }
        }

        public async Task<List<TipoEnderecoResponse>> ObterDescricaoTipoEndereco(MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                return (await conn.QueryAsync<TipoEnderecoResponse>(
                            EnderecoQuerys.SELECT_TIPO_ENDERECO)).ToList();
            }
            else
            {
                return (await connection.QueryAsync<TipoEnderecoResponse>(
                            EnderecoQuerys.SELECT_TIPO_ENDERECO)).ToList();
            }
        }

        public async Task<List<Endereco>> ObterEnderecoFornecedor(int fornecedor_id, MySqlConnection conn = null)
        {
            List<Endereco> ret = new();
            List<TipoEnderecoResponse> tiposEnd = new();
            if (conn is null)
            {
                using var connection = await _connection.GetConnectionAsync();
                var enderecosIds = await connection.QueryAsync<int>(
                        sql: EnderecoQuerys.SELECT_ID_ENDERECO_FORNECEDOR,
                        param: new { fornecedor_id });

                if (enderecosIds.Any())
                    foreach (var item in enderecosIds)
                        ret.Add(connection.Get<Endereco>(item));

                tiposEnd = (await connection.QueryAsync<TipoEnderecoResponse>(
                        EnderecoQuerys.SELECT_TIPO_ENDERECO)).ToList();
            }
            else
            {
                var enderecosIds = await conn.QueryAsync<int>(
                        sql: EnderecoQuerys.SELECT_ID_ENDERECO_FORNECEDOR,
                        param: new { fornecedor_id });

                if (enderecosIds.Any())
                    foreach (var item in enderecosIds)
                        ret.Add(conn.Get<Endereco>(item));

                tiposEnd = (await conn.QueryAsync<TipoEnderecoResponse>(
                        EnderecoQuerys.SELECT_TIPO_ENDERECO)).ToList();
            }
            foreach (var endereco in ret)
                endereco.Tipo_endereco = tiposEnd
                        .Where(x => x.Id == endereco.Tipo_endereco_id)
                        .Select(x => x.Descricao).FirstOrDefault();
            return ret;
        }

        public async Task<Endereco> GetById(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var enderecoBanco = connection.Get<Endereco>(id);
            if (enderecoBanco is null)
                return null;
            enderecoBanco.TiposEndereco = await this.ObterDescricaoTipoEndereco(connection);
            return enderecoBanco;
        }

        public async Task<bool> AtualizarEndereco(EnderecoRequest endereco, MySqlConnection connection = null)
        {
           if(connection is null)
           {
                using var conn = await _connection.GetConnectionAsync();
                var enderecoBanco = conn.Get<Endereco>(endereco.Endereco_id);
                if (enderecoBanco is null)
                    return false;
                enderecoBanco.Logradouro = string.IsNullOrEmpty(endereco.Logradouro) ? enderecoBanco.Logradouro : endereco.Logradouro.ToUpper();
                enderecoBanco.Numero = string.IsNullOrEmpty(endereco.Numero) ? enderecoBanco.Numero : endereco.Numero.ToUpper();
                enderecoBanco.Complemento = string.IsNullOrEmpty(endereco.Complemento) ? enderecoBanco.Complemento : endereco.Complemento.ToUpper();
                enderecoBanco.Cep = string.IsNullOrEmpty(endereco.Cep) ? enderecoBanco.Cep : endereco.Cep;
                enderecoBanco.Bairro = string.IsNullOrEmpty(endereco.Bairro) ? enderecoBanco.Bairro : endereco.Bairro.ToUpper();
                enderecoBanco.Cidade = string.IsNullOrEmpty(endereco.Cidade) ? enderecoBanco.Cidade : endereco.Cidade.ToUpper();
                enderecoBanco.Estado = string.IsNullOrEmpty(endereco.Estado) ? enderecoBanco.Estado : endereco.Estado.ToUpper();
                enderecoBanco.UF = string.IsNullOrEmpty(endereco.Uf) ? enderecoBanco.UF : endereco.Uf.ToUpper();
                enderecoBanco.Ativo = 1;
                enderecoBanco.Data_alteracao = DateTime.Now;
                return conn.Update<Endereco>(enderecoBanco);
            }
            else
            {
                var enderecoBanco = connection.Get<Endereco>(endereco.Endereco_id);
                if (enderecoBanco is null)
                    return false;
                enderecoBanco.Logradouro = string.IsNullOrEmpty(endereco.Logradouro) ? enderecoBanco.Logradouro : endereco.Logradouro.ToUpper();
                enderecoBanco.Numero = string.IsNullOrEmpty(endereco.Numero) ? enderecoBanco.Numero : endereco.Numero.ToUpper();
                enderecoBanco.Complemento = string.IsNullOrEmpty(endereco.Complemento) ? enderecoBanco.Complemento : endereco.Complemento.ToUpper();
                enderecoBanco.Cep = string.IsNullOrEmpty(endereco.Cep) ? enderecoBanco.Cep : endereco.Cep;
                enderecoBanco.Bairro = string.IsNullOrEmpty(endereco.Bairro) ? enderecoBanco.Bairro : endereco.Bairro.ToUpper();
                enderecoBanco.Cidade = string.IsNullOrEmpty(endereco.Cidade) ? enderecoBanco.Cidade : endereco.Cidade.ToUpper();
                enderecoBanco.Estado = string.IsNullOrEmpty(endereco.Estado) ? enderecoBanco.Estado : endereco.Estado.ToUpper();
                enderecoBanco.UF = string.IsNullOrEmpty(endereco.Uf) ? enderecoBanco.UF : endereco.Uf.ToUpper();
                enderecoBanco.Ativo = 1;
                enderecoBanco.Data_alteracao = DateTime.Now;
                return connection.Update<Endereco>(enderecoBanco);
            }
        }

        public async Task<bool> ExcluirEnderecoFornecedor(int fornecedor_id, int endereco_id, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                await conn.QueryAsync(
                    sql: EnderecoQuerys.DESATIVAR_ENDERECO_FORNECEDOR,
                    param: new { fornecedor_id, endereco_id });
                return true;
            }
            else
            {
                await connection.QueryAsync(
                sql: EnderecoQuerys.DESATIVAR_ENDERECO_FORNECEDOR,
                param: new { fornecedor_id, endereco_id });
                return true;
            }
        }
    }
}