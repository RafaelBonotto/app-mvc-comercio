using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Comercio.Interfaces.FornecedorInterfaces;
using MySqlConnector;
using Comercio.Data.Repositories.Response;
using Comercio.Interfaces.TelefoneInterfaces;

namespace Comercio.Data.Repositories.Fornecedores
{
    public class FornecedorRepository : IRepositoryBase<Fornecedor>, IFornecedorRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IFornecedorAdapter _mapper;
        private readonly ITelefoneRepository _telefoneRepository;


        public FornecedorRepository(
            IMySqlConnectionManager connection, 
            IFornecedorAdapter mapper,
            ITelefoneRepository telefoneRepository)
        {
            _connection = connection;
            _mapper = mapper;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<Fornecedor> AddAsync(Fornecedor fornecedor)
        {
            try
            {
                int fornecdorId;
                using (var connection = await _connection.GetConnectionAsync())
                {
                    fornecdorId = await connection.InsertAsync<Fornecedor>(fornecedor);
                    if (fornecdorId <= 0)
                        throw new Exception("Erro ao tentar inserir o fornecedor");
                }
                return await this.GetByIdAsync(fornecdorId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Fornecedor> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fornecedor>> GetAllAsync()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var fornecedor = await connection.GetAllAsync<Fornecedor>();
                return fornecedor.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var fornecedor = connection.Get<Fornecedor>(id);
                fornecedor.Telefone = await RetornarTelefoneDoFornecedor(fornecedor.Id, connection);
                fornecedor.Endereco = await RetornarEnderecoDoFornecedor(fornecedor.Id, connection);
                //fornecedor.Vendedor = await RetornarVendedorDoFornecedor(fornecedor.Id, connection);
                return fornecedor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Fornecedor>> GetByKeyAsync(string cnpj)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                return (connection.Query<Fornecedor>(FornecedorQuerys.SELECT_POR_CNPJ, new { cnpj })).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InserirEndereco(int fornecedor_id, Endereco endereco)
        {
            try
            {
                using (var connection = await _connection.GetConnectionAsync())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var endereco_id = await connection.InsertAsync<Endereco>(endereco, transaction);
                            if (endereco_id <= 0)
                                throw new Exception("Erro ao tentar inserir o endereço do fornecedor");

                            var row = await connection.InsertAsync<EnderecoFornecedor>(
                                _mapper.MontaEnderecoFornecedor(fornecedor_id, endereco_id),transaction);
                            if (row <= 0)
                            {
                                transaction.Rollback();
                                throw new Exception("Erro ao tentar inserir o endereço do fornecedor");
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InserirTelefone(int fornecedor_id, Telefone telefone)
        {
            try
            {
                return await _telefoneRepository.InserirTelefoneFornecedor(fornecedor_id, telefone);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task InserirVendedor(int fornecedor_id, Vendedor vendedor)
        {
            //try
            //{
            //    foreach (var vendedor in vendedores)
            //    {
            //        var pessoa_id = await connection.InsertAsync<Pessoa>(vendedor);
            //        if (pessoa_id <= 0)
            //            throw new Exception("Erro ao tentar inserir o vendedor");

            //        var row = await connection.InsertAsync<FornecedorVendedor>(new FornecedorVendedor()
            //        {
            //            Fornecedor_id = fornecedor_id,
            //            Pessoa_id = pessoa_id
            //        });
            //        if (row <= 0)
            //            throw new Exception("Erro ao tentar inserir o vendedor");
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ObterIdTipoTelefone(string tipoTelefone)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                //return connection.QueryFirstOrDefault<int>(
                //    FornecedorQuerys.SELECT_ID_TIPO_TELEFONE, new { tipoTelefone });
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TipoTelefoneResponse>> ObterDescricaoTipoTelefone()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                //return (await connection.QueryAsync<TipoTelefoneResponse>(
                //    FornecedorQuerys.SELECT_TIPO_TELEFONE)).ToList();
                return new List<TipoTelefoneResponse>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ObterIdTipoEndereco(string tipoEndereco)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                return connection.QueryFirstOrDefault<int>(
                    FornecedorQuerys.SELECT_ID_TIPO_ENDERECO, new { tipoEndereco});
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TipoEnderecoResponse>> ObterTipoEndereco()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                return (await connection.QueryAsync<TipoEnderecoResponse>(
                    FornecedorQuerys.SELECT_TIPO_ENDERECO)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ExcluirTelefone(int fornecedor_id, int telefone_id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                //await connection.QueryAsync(
                //    sql: FornecedorQuerys.DESATIVAR_TELEFONE_FORNECEDOR,
                //    param: new { fornecedor_id, telefone_id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Métodos privados

        public static async Task<List<Telefone>> RetornarTelefoneDoFornecedor(int fornecedor_id, MySqlConnection connection)
        {
            List<Telefone> ret = new();
            //var telefoneIds = await connection.QueryAsync<int>(
            //        sql: FornecedorQuerys.SELECT_ID_TELEFONE_FORNECEDOR,
            //        param: new { fornecedor_id });
            //if(telefoneIds.Any())
            //    foreach (var item in telefoneIds)
            //        ret.Add(connection.Get<Telefone>(item));

            //var tipoTelefoneDesc = (await connection.QueryAsync<TipoTelefoneResponse>(
            //        FornecedorQuerys.SELECT_TIPO_TELEFONE)).ToList();

            //foreach (var telef in ret)
            //    telef.Tipo_telefone = tipoTelefoneDesc
            //            .Where(x => x.Id == telef.Tipo_telefone_id)
            //            .Select(x => x.Descricao).FirstOrDefault();
            return ret;
        }

        static async Task<List<Endereco>> RetornarEnderecoDoFornecedor(int fornecedor_id, MySqlConnection connection)
        {
            List<Endereco> ret = new();
            var enderecosIds = await connection.QueryAsync<int>(
                    sql: FornecedorQuerys.SELECT_ID_ENDERECO_FORNECEDOR,
                    param: new { fornecedor_id });
            if(enderecosIds.Any())
                foreach (var item in enderecosIds)
                    ret.Add(connection.Get<Endereco>(item));
            return ret;
        }

        static async Task<List<Vendedor>> RetornarVendedorDoFornecedor(int fornecedor_id, MySqlConnection connection)
        {
            List<Vendedor> ret = new();
            var vendedorIds = await connection.QueryAsync<int>(
                    sql: FornecedorQuerys.SELECT_ID_VENDEDOR_FORNECEDOR,
                    param: new { fornecedor_id });
            if(vendedorIds.Any())
                foreach (var item in vendedorIds)
                    ret.Add((Vendedor)connection.Get<Pessoa>(item));
            return ret;
        }
        
        #endregion
    }
}
