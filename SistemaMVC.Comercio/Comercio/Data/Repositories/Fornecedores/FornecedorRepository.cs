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
                using var connection = await _connection.GetConnectionAsync();
                //1- INSERIR FORNECEDOR
                var fornecdorId = await connection.InsertAsync<Fornecedor>(fornecedor);
                //2- INSERIR ENDERECO
                await InserirEndereco(fornecdorId, fornecedor.Endereco, connection);
                //4- INSERIR TELEFONE
                await InserirTelefone(fornecdorId, fornecedor.Telefone, connection);
                //6- INSERIR VENDEDOR (PESSOA)
                await InserirVendedor(fornecdorId, fornecedor.Vendedor, connection);
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

        public Task<Fornecedor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fornecedor>> GetByKeyAsync(string cnpj)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                List<Produto> ret = new();
                return (connection.Query<Fornecedor>(FornecedorQuerys.SELECT_POR_CNPJ, new { cnpj })).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> InserirEndereco(int fornecedor_id, List<Endereco> enderecos, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InserirEnderecoFornecedor(List<FornecedorEndereco> fornecedorEndereco, MySqlConnection connection)
        {
            try
            {
                foreach (var item in fornecedorEndereco)
                    await connection.InsertAsync<FornecedorEndereco>(item);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> InserirTelefone(int fornecedor_id, List<Telefone> telefones, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        static Task<bool> InserirTelefoneFornecedor(List<FornecedorTelefone> fornecedorTelefone, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task<int> InserirVendedor(int fornecedor_id, List<Vendedor> vendedores, MySqlConnection connection) 
        {
            throw new NotImplementedException();
        }

        static Task<bool> InserirVendedorFornecedor(int fornecedor_id, List<Vendedor> vendedor, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }
    }
}
