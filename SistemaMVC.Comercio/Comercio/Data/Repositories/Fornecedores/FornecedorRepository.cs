using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Enums;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Requests.Fornecedor;
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
            using (var connection = await _connection.GetConnectionAsync())
            {
                var fornecdorId = await connection.InsertAsync<Fornecedor>(fornecedor);
                if (fornecdorId <= 0)
                    return null;
                return await this.GetFornecedorAsync(fornecedor.Id, connection);
            }
        }

        public async Task<Fornecedor> UpdateAsync(Fornecedor fornecedor)
        {
            using (var connection = await _connection.GetConnectionAsync())
            {
                var fornecedorBanco = await connection.GetAsync<Fornecedor>(fornecedor.Id);
                fornecedorBanco.Nome_empresa = !string.IsNullOrEmpty(fornecedor.Nome_empresa) ? fornecedor.Nome_empresa.ToUpper() : fornecedorBanco.Nome_empresa;
                fornecedorBanco.Email = !string.IsNullOrEmpty(fornecedor.Email) ? fornecedor.Email.ToLower() : fornecedorBanco.Email;
                fornecedorBanco.Data_alteracao = System.DateTime.Now;
                var update = await connection.UpdateAsync<Fornecedor>(fornecedorBanco);
                if (!update)
                    return null;
                return await this.GetFornecedorAsync(fornecedor.Id, connection);
            }
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
            fornecedor.Telefone = await _telefoneRepository.ListarTelefoneFornecedor(id, connection);
            fornecedor.Endereco = await _enderecoRepository.ObterEnderecoFornecedor(fornecedor.Id, connection);
            fornecedor.Vendedor = await RetornarVendedorDoFornecedor(fornecedor.Id, connection);
            fornecedor.DescricaoTipoEndereco = await _enderecoRepository.ObterDescricaoTipoEndereco(connection);
            fornecedor.DescricaoTipoTelefone = await _telefoneRepository.ListarDescricaoTipoTelefone(connection);
            return fornecedor;
        }

        public async Task<List<Fornecedor>> GetByKeyAsync(string cnpj)
        {
            using var connection = await _connection.GetConnectionAsync();
            return (connection.Query<Fornecedor>(
                FornecedorQuerys.SELECT_POR_CNPJ, new { cnpj })).ToList();
        }

        public async Task<bool> ExcluirFornecedor(int id)
        {
            using (var connection = await _connection.GetConnectionAsync())
            {
                var fornecedorBanco = await connection.GetAsync<Fornecedor>(id);
                if (fornecedorBanco is null)
                    return false;

                await connection.QueryAsync(
                    sql: FornecedorQuerys.DESATIVAR_FORNECEDOR,
                    param: new { id });

                return true;
            }
        }

        public async Task<Fornecedor> InserirEndereco(EnderecoRequest req)
        {
            using var connection = await _connection.GetConnectionAsync();
            var endereco = _mapper.MontarInsertEndereco(req);
            endereco.Tipo_endereco_id = await _enderecoRepository.ObterIdTipoEndereco(req.Tipo_endereco, connection);
            var insert = await _enderecoRepository.InserirEnderecoFornecedor(req.Fornecedor_id, endereco, connection);
            if (!insert)
                return null;
            return await GetFornecedorAsync(req.Fornecedor_id, connection);
        }

        public async Task<Fornecedor> InserirVendedor(int fornecedor_id, PessoaContato vendedor, List<Telefone> telefones)
        {
            using var connection = await _connection.GetConnectionAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var vendedor_id = await connection.InsertAsync<PessoaContato>(vendedor, transaction);
                if (vendedor_id <= 0)
                    return null;

                var vendedorFornecedorId = await connection.InsertAsync<PessoaContatoFornecedor>(
                    entityToInsert: _mapper.MontaInsertVendedorFornecedor(fornecedor_id, vendedor_id), 
                    transaction: transaction);

                if (vendedor_id <= 0)
                {
                    transaction.Rollback();
                    return null;
                }

                foreach (var telefone in telefones)
                {
                    var telefone_id = await connection.InsertAsync<Telefone>(telefone, transaction);
                    if (telefone_id <= 0)
                    {
                        transaction.Rollback();
                        return null;
                    }

                    var vendedorTelefoneId = await connection.InsertAsync<PessoaContatoTelefone>(
                        entityToInsert: _mapper.MontaInsertVendedorTelefone(vendedor_id, telefone_id), transaction);
                    if (vendedorTelefoneId <= 0)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
                transaction.Commit();
                return await GetFornecedorAsync(fornecedor_id, connection);
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<Fornecedor> AtualizarVendedor(VendedorRequest vendedor)
        {
            using var connection = await _connection.GetConnectionAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var vendedorBanco = await connection.GetAsync<PessoaContato>(vendedor.Vendedor_id, transaction);
                vendedorBanco.Nome = vendedor.Nome.ToUpper();
                vendedorBanco.Email = vendedor.Email;
                vendedorBanco.Ativo = 1;
                vendedorBanco.Data_alteracao = DateTime.Now;
                var update = await connection.UpdateAsync<PessoaContato>(vendedorBanco, transaction);
                if (!update)
                    return null;

                var telefones = await this.GetTelefoneVendedor(vendedorBanco.Id, connection, transaction);
                foreach (var telefone in telefones)
                {
                    if (telefone.Tipo_telefone_id == TipoTelefone.ADICIONAL.GetHashCode())
                    {
                        telefone.Ddd = vendedor.DddAdicional;
                        telefone.Numero = vendedor.NumeroAdicional;
                    }
                    else
                    {
                        telefone.Ddd = vendedor.Ddd;
                        telefone.Numero = vendedor.Numero;
                    }
                    telefone.Ativo = 1;
                    telefone.Data_alteracao = DateTime.Now;
                    var updateTelefone = await connection.UpdateAsync<Telefone>(telefone, transaction);
                    if (!updateTelefone)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
                transaction.Commit();
                return await this.GetFornecedorAsync(vendedor.Fornecedor_id, connection);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<Fornecedor> ExcluirVendedor(int fornecedor_id, int vendedor_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var vendedor = await connection.QueryFirstOrDefaultAsync<PessoaContatoFornecedor>(
                sql: FornecedorQuerys.SELECT_VENDEDOR,
                param: new { fornecedor_id, vendedor_id });
            if(vendedor is null)
                return null;

            vendedor.Ativo = 0;
            vendedor.Data_alteracao = DateTime.Now;
            var update = await connection.UpdateAsync<PessoaContatoFornecedor>(vendedor);
            if (!update)
                return null;

            return await this.GetFornecedorAsync(fornecedor_id, connection);
        }

        public async Task<PessoaContato> GetVendedor(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var vendedor = connection.Get<PessoaContato>(id);
            if (vendedor is null)
                return null;
            vendedor.Telefones = await this.GetTelefoneVendedor(id, connection);
            return vendedor;
        }

        public async Task<List<Telefone>> GetTelefoneVendedor(int vendedor_id, MySqlConnection connection = null, MySqlTransaction transaction = null)
        {
            List<Telefone> ret = new();
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                var telefoneIds = await conn.QueryAsync<int>(
                        sql: FornecedorQuerys.SELECT_ID_TELEFONE_VENDEDOR,
                        param: new { vendedor_id },
                        transaction: transaction);
                if (telefoneIds.Any())
                    foreach (var id in telefoneIds)
                        ret.Add(conn.Get<Telefone>(id));
            }
            else
            {
                var telefoneIds = await connection.QueryAsync<int>(
                        sql: FornecedorQuerys.SELECT_ID_TELEFONE_VENDEDOR,
                        param: new { vendedor_id },
                        transaction: transaction);
                if (telefoneIds.Any())
                    foreach (var id in telefoneIds)
                        ret.Add(connection.Get<Telefone>(id, transaction: transaction));
            }
            return ret;
        }

        public async Task<Fornecedor> InserirTelefone(int fornecedor_id, Telefone telefone, MySqlConnection connection = null)
        {
            if(connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                var insert = await _telefoneRepository.InserirTelefoneFornecedor(fornecedor_id, telefone, conn);
                if (!insert)
                    return null;
                return await GetFornecedorAsync(fornecedor_id, conn);
            }
            else
            {
                var insert = await _telefoneRepository.InserirTelefoneFornecedor(fornecedor_id, telefone, connection);
                if (!insert)
                    return null;
                return await GetFornecedorAsync(fornecedor_id, connection);
            }
        }

        public async Task<Fornecedor> EditarTelefone(TelefoneRequest telefone, MySqlConnection connection = null)
        {
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                telefone.Tipo_telefone_id = await _telefoneRepository.ObterIdTipoTelefone(telefone.Tipo_telefone, conn);
                var updateTelefone = await _telefoneRepository.AtualizarTelefone(telefone, conn);
                if (!updateTelefone)
                    return null;

                return await GetFornecedorAsync(telefone.Fornecedor_id, conn);
            }
            telefone.Tipo_telefone_id = await _telefoneRepository.ObterIdTipoTelefone(telefone.Tipo_telefone, connection);
            var update = await _telefoneRepository.AtualizarTelefone(telefone, connection);
            if (!update)
                return null;

            return await GetFornecedorAsync(telefone.Fornecedor_id, connection);
        }

        public async Task<Fornecedor> EditarEndereco(EnderecoRequest req)
        {
            using var conn = await _connection.GetConnectionAsync();
            req.Tipo_endereco_id = await _enderecoRepository.ObterIdTipoEndereco(req.Tipo_endereco, conn);
            var updateEndereco = await _enderecoRepository.AtualizarEndereco(req, conn);
            if (!updateEndereco)
                return null;

            return await GetFornecedorAsync(req.Fornecedor_id, conn);
        }
        public async Task<Fornecedor> ExcluirEndereco(int fornecedor_id, int endereco_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var delete = await _enderecoRepository.ExcluirEnderecoFornecedor(fornecedor_id, endereco_id, connection);
            if (!delete)
                return null;
            return await GetFornecedorAsync(fornecedor_id, connection);
        }

        public async Task<Fornecedor> ExcluirTelefone(int fornecedor_id, int telefone_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var delete = await _telefoneRepository.ExcluirTelefoneFornecedor(fornecedor_id, telefone_id, connection);
            if (!delete)
                return null;
            return await GetFornecedorAsync(fornecedor_id, connection);
        }

        #region Métodos privados

        static async Task<List<PessoaContato>> RetornarVendedorDoFornecedor(int fornecedor_id, MySqlConnection connection)
        {
            List<PessoaContato> ret = new();
            var vendedorIds = await connection.QueryAsync<int>(
                  sql: FornecedorQuerys.SELECT_ID_VENDEDOR_FORNECEDOR,
                  param: new { fornecedor_id });
            if (vendedorIds.Any())
            {
                foreach (var id in vendedorIds)
                {
                    var vendedor = connection.Get<PessoaContato>(id);
                    var telefoneIds = await connection.QueryAsync<int>(
                        sql: FornecedorQuerys.SELECT_ID_TELEFONE_VENDEDOR,
                        param: new { vendedor_id = id });

                    if (telefoneIds.Any())
                        foreach (var telefoneId in telefoneIds)
                            vendedor.Telefones.Add(connection.Get<Telefone>(telefoneId));
                    ret.Add(vendedor);
                }
            }
            return ret;
        }

        public async Task<List<Produto>> ListarProdutos(int fornecedor_id)
        {
            List<Produto> ret = new();
            using var connection = await _connection.GetConnectionAsync();

            var produtoIds = await connection.QueryAsync<int>(
                sql: FornecedorQuerys.SELECT_PRODUTO_ID,
                param: new { fornecedor_id });

            if (produtoIds.Any())
            {
                foreach (var id in produtoIds)
                    ret.Add(await connection.QueryFirstOrDefaultAsync<Produto>(
                          sql: FornecedorQuerys.SELECT_PRODUTOS,
                          param: new { id }));
            }
            return ret;
        }

        private async Task<Fornecedor> GetFornecedorAsync(int id, MySqlConnection connection)
        {
            var fornecedor = connection.Get<Fornecedor>(id);
            fornecedor.Telefone = await _telefoneRepository.ListarTelefoneFornecedor(id, connection);
            fornecedor.Endereco = await _enderecoRepository.ObterEnderecoFornecedor(fornecedor.Id, connection);
            fornecedor.Vendedor = await RetornarVendedorDoFornecedor(fornecedor.Id, connection);
            fornecedor.DescricaoTipoEndereco = await _enderecoRepository.ObterDescricaoTipoEndereco(connection);
            fornecedor.DescricaoTipoTelefone = await _telefoneRepository.ListarDescricaoTipoTelefone(connection);
            return fornecedor;
        }
        #endregion
    }
}
