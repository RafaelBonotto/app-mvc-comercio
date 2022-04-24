﻿using Comercio.Data.ConnectionManager;
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

                //3- INSERIR ENDERECO - FORNECEDOR
                //await _repository.InserirEnderecoFornecedor(_mapper.MontaFornecedorEndereco(fornecedor));

                //4- INSERIR TELEFONE

                //5- INSERIR TELEFONE FORNECEDOR
                //await _repository.InserirTelefoneFornecedor(_mapper.MontaFornecedorTelefone(fornecedor));

                //6- INSERIR VENDEDOR (PESSOA)

                //4- INSERIR VENDEDOR - FORNECEDOR
                //await _repository.InserirVendedorFornecedor(_mapper)

                return null;
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

        public Task<bool> InserirTelefoneFornecedor(List<FornecedorTelefone> fornecedorTelefone, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InserirVendedorFornecedor(int fornecedor_id, List<Vendedor> vendedor, MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }
    }
}
