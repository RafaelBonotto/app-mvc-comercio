using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Fornecedores
{
    public class FornecedorRepository : IRepositoryBase<Fornecedor>, IFornecedorRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IFornecedorAdapter _mapper;
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorRepository(
            IMySqlConnectionManager connection, 
            IFornecedorAdapter mapper,
            ITelefoneRepository telefoneRepository,
            IEnderecoRepository enderecoRepository)
        {
            _connection = connection;
            _mapper = mapper;
            _telefoneRepository = telefoneRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Fornecedor> AddAsync(Fornecedor fornecedor)
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

        public Task<Fornecedor> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fornecedor>> GetAllAsync()
        {
            using var connection = await _connection.GetConnectionAsync();
            var fornecedor = await connection.GetAllAsync<Fornecedor>();
            return fornecedor.ToList();
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var fornecedor = connection.Get<Fornecedor>(id);
            fornecedor.Telefone = await _telefoneRepository.ListarTelefoneFornecedor(id);
            fornecedor.Endereco = await _enderecoRepository.ObterEnderecoFornecedor(fornecedor.Id);
            //fornecedor.Vendedor = await RetornarVendedorDoFornecedor(fornecedor.Id, connection);
            return fornecedor;
        }

        public async Task<List<Fornecedor>> GetByKeyAsync(string cnpj)
        {
            using var connection = await _connection.GetConnectionAsync();
            return (connection.Query<Fornecedor>(FornecedorQuerys.SELECT_POR_CNPJ, new { cnpj })).ToList();
        }

        public async Task<bool> InserirVendedor(int fornecedor_id, PessoaContato vendedor, List<Telefone> telefones)
        {
            using var connection = await _connection.GetConnectionAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var vendedor_id = await connection.InsertAsync<PessoaContato>(vendedor, transaction);
                if (vendedor_id <= 0)
                    return false;

                var vendedorFornecedorId = await connection.InsertAsync<PessoaContatoFornecedor>(
                    entityToInsert: _mapper.MontaInsertVendedorFornecedor(fornecedor_id, vendedor_id), transaction);
                if (vendedor_id <= 0)
                {
                    transaction.Rollback();
                    return false;
                }

                foreach (var telefone in telefones)
                {
                    var telefone_id = await connection.InsertAsync<Telefone>(telefone, transaction);
                    if (telefone_id <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    var vendedorTelefoneId = await connection.InsertAsync<PessoaContatoTelefone>(
                        entityToInsert: _mapper.MontaInsertVendedorTelefone(vendedor_id, telefone_id), transaction);
                    if (vendedorTelefoneId <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public Task<Fornecedor> UpdateAsync(Fornecedor entity)
        {
            throw new NotImplementedException();
        }

        

        #region Métodos privados
        
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
