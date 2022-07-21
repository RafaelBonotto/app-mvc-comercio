using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Responses.Produto;
using Dapper;
using Dapper.Contrib.Extensions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Produtos
{
    public class ProdutoRepository : IRepositoryBase<Produto>, IProdutoRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IFornecedorRepository _fornecedorRepository;

        public ProdutoRepository(
            IMySqlConnectionManager connection,
            IFornecedorRepository fornecedorRepository)
        {
            _connection = connection;
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();                           
                var row = await connection.InsertAsync<Produto>(produto);
                if (row > 0)
                    return await this.GetByIdAsync(row);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produto = await this.GetByIdAsync(id);
            produto.Ativo = 0;
            produto.Data_alteracao = DateTime.Now;
            var response = connection.Update<Produto>(produto);
            if (!response)
                return null;
            return produto;
        }

        public async Task<List<Produto>> FiltrarPorDescricao(string descricao)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                                sql: ProdutoQuerys.SELECT_POR_DESCRICAO,
                                (produto, setor) =>
                                {
                                    produto.Setor = setor;
                                    return produto;
                                },
                                param: new { descricao }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> FiltrarPorSetor(string setor)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = connection.Query<Produto, Setor, Produto>(
                                sql: ProdutoQuerys.SELECT_POR_SETOR,
                                (produto, setor) =>
                                {
                                    produto.Setor = setor;
                                    return produto;
                                },
                                param: new { setor }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<Produto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> GetByIdAsync(int produto_id)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var produto = connection.Query<Produto, Setor, Produto>(
                            sql: ProdutoQuerys.SELECT_POR_ID,
                            (produto, setor) =>
                            {
                                produto.Setor = setor;
                                return produto;
                            },
                            param: new { produto_id }).FirstOrDefault();

                produto.Fornecedores = await MontaFornecedoresDoProduto(connection);
                return produto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Produto>> GetByKeyAsync(string codigo)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                List<Produto> ret = new();
                var produtos = connection.Query<Produto, Setor, Produto>(
                                    sql: ProdutoQuerys.SELECT_POR_CODIGO,
                                    (produto, setor) =>
                                    {
                                        produto.Setor = setor;
                                        return produto;
                                    },
                                    param: new { codigo }).ToList();
                if (produtos.Any())
                    foreach (var produto in produtos)
                        produto.Fornecedores = await this.MontaFornecedoresDoProduto(connection);

                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Setor>> ObterSetores()
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var setores = await connection.QueryAsync<Setor>(ProdutoQuerys.SELECT_LISTAR_SETORES);
                foreach (var setor in setores)
                    setor.Descricao = setor.Descricao.ToUpper();
                return setores.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                var response = await connection.UpdateAsync<Produto>(produto);
                if (!response)
                    return null;
                return await this.GetByIdAsync(produto.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ObterSetorId(string setor)
        {
            try
            {
                using var connection = await _connection.GetConnectionAsync();
                return connection.QueryFirst<int>(
                                ProdutoQuerys.SELECT_ID_SETOR,
                                new { descricao = setor });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Fornecedor>> ObterFornecedor(int produto_id)
        {
            List<Fornecedor> ret = new();
            using var connection = await _connection.GetConnectionAsync();
            var fornecedorIds = await connection.QueryAsync<int>(
                ProdutoQuerys.SELECT_ID_FORNECEDOR_POR_PRODUTO, new { produto_id });

            if (fornecedorIds.Any())
                foreach (var id in fornecedorIds)
                    ret.Add(await _fornecedorRepository.GetFornecedorAsync(id, connection));

            return ret;
        }

        public async Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var fornecedor = await _fornecedorRepository.GetFornecedorAsync(fornecedor_id, connection);
            if (fornecedor is null)
                return null;

            return fornecedor;
        }

        private async Task<List<Fornecedor>> MontaFornecedoresDoProduto(MySqlConnection connection)
        {
            List<Fornecedor> ret = new();
            var fornecedoresResponse = await connection.QueryAsync<ListaFornecedorResponse>(
                    sql: ProdutoQuerys.SELECT_LISTA_FORNECEDORES);

            if (fornecedoresResponse.Any())
            {
                foreach (var fornecedor in fornecedoresResponse)
                {
                    ret.Add(new Fornecedor()
                    {
                        Id = fornecedor.Id,
                        Cnpj = fornecedor.Cnpj,
                        Nome_empresa = $"{fornecedor.Cnpj} - {fornecedor.Nome_empresa}"
                    });
                }
            }
            return ret;
        }
    }
}
