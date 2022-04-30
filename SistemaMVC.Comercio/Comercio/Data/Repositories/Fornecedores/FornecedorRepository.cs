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

namespace Comercio.Data.Repositories.Fornecedores
{
    public class FornecedorRepository : IRepositoryBase<Fornecedor>, IFornecedorRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IFornecedorAdapter _mapper;


        public FornecedorRepository(IMySqlConnectionManager connection, IFornecedorAdapter mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<Fornecedor> AddAsync(Fornecedor fornecedor)
        {
            try
            {
                int fornecdorId;
                using (var connection = await _connection.GetConnectionAsync())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            fornecdorId = await connection.InsertAsync<Fornecedor>(fornecedor);
                            if (fornecdorId <= 0)
                                throw new Exception("Erro ao tentar inserir o fornecedor");
                            await InserirEndereco(fornecdorId, fornecedor.Endereco, connection);
                            await InserirTelefone(fornecdorId, fornecedor.Telefone, connection);
                            await InserirVendedor(fornecdorId, fornecedor.Vendedor, connection);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
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

        public Task<List<Fornecedor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var fornecedor = connection.Get<Fornecedor>(id);

                
                

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

        public async Task InserirEndereco(int fornecedor_id, List<Endereco> enderecos, MySqlConnection connection)
        {
            try
            {
                foreach (var endereco in enderecos)
                {
                    var endereco_id = await connection.InsertAsync<Endereco>(endereco);
                    if (endereco_id <= 0)
                        throw new Exception("Erro ao tentar inserir o endereço do fornecedor");
                    var row = await connection.InsertAsync<FornecedorEndereco>(new FornecedorEndereco()
                    {
                        Fornecedor_id = fornecedor_id,
                        Endereco_id = endereco_id
                    });
                    if (row <= 0)
                        throw new Exception("Erro ao tentar inserir o endereço do fornecedor");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InserirTelefone(int fornecedor_id, List<Telefone> telefones, MySqlConnection connection)
        {
            try
            {
                foreach (var telefone in telefones)
                {
                    var telefone_id = await connection.InsertAsync<Telefone>(telefone);
                    if (telefone_id <= 0)
                        throw new Exception("Erro ao tentar inserir o telefobe do fornecedor");

                    var row = await connection.InsertAsync<FornecedorTelefone>(new FornecedorTelefone()
                    {
                        Fornecedor_id = fornecedor_id,
                        Telefone_id = telefone_id
                    });
                    if (telefone_id <= 0)
                        throw new Exception("Erro ao tentar inserir o telefobe do fornecedor");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task InserirVendedor(int fornecedor_id, List<Vendedor> vendedores, MySqlConnection connection) 
        {
            try
            {
                foreach (var vendedor in vendedores)
                {
                    var pessoa_id = await connection.InsertAsync<Pessoa>(vendedor);
                    if (pessoa_id <= 0)
                        throw new Exception("Erro ao tentar inserir o vendedor");

                    var row = await connection.InsertAsync<FornecedorVendedor>(new FornecedorVendedor()
                    {
                        Fornecedor_id = fornecedor_id,
                        Pessoa_id = pessoa_id
                    });
                    if (row <= 0)
                        throw new Exception("Erro ao tentar inserir o vendedor");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        #region Métodos privados

        static async Task<List<Telefone>> RetornarTelefonesDoFornecedor(int fornecedor_id, MySqlConnection connection)
        {
            List<Telefone> ret = new();
            var TelefoneIds = await connection.QueryAsync<int>(
                    sql: FornecedorQuerys.SELECT_ID_TELEFONE_FORNECEDOR,
                    param: new { fornecedor_id = fornecedor_id });
            foreach (var item in TelefoneIds)
                ret.Add(connection.Get<Telefone>(item));
            return ret;
        }

        #endregion
    }
}
