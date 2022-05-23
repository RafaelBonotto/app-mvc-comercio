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

        public async Task InserirVendedor(int fornecedor_id, Vendedor vendedor)
        {
            //foreach (var vendedor in vendedores)
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
