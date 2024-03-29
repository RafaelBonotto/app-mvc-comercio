﻿using Comercio.Data.ConnectionManager;
using Comercio.Data.Querys;
using Comercio.Entities;
using Comercio.Exceptions.Produto;
using Comercio.Interfaces;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
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
            using var connection = await _connection.GetConnectionAsync();
            produto.Setor_id = await connection.QueryFirstAsync<int>(
                       sql: ProdutoQuerys.SELECT_ID_SETOR,
                       param: new { descricao = produto.Setor.Descricao });

            var codigoExiste = await GetByKeyAsync(produto.Codigo);
            if (codigoExiste != null)
            {
                var produtoBanco = codigoExiste.First();
                if (produtoBanco.Ativo == 1)
                    throw new CodigoInvalidoException();

                if (produtoBanco.Ativo == 0)
                {
                    produtoBanco.Ativo = 1;
                    produtoBanco.Data_alteracao = DateTime.Now;
                    produtoBanco.Descricao = produto.Descricao;
                    produtoBanco.Preco_custo = produto.Preco_custo;
                    produtoBanco.Preco_venda = produto.Preco_venda;
                    produtoBanco.Setor_id = produto.Setor_id;
                    var update = await connection.UpdateAsync<Produto>(produtoBanco);
                    if (!update)
                        return null;

                    return await GetProdutoAsync(produtoBanco.Id, connection);
                }
            }
            var row = await connection.InsertAsync<Produto>(produto);
            if (row > 0)
                return await GetProdutoAsync(row, connection);

            return null;
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produto = await connection.GetAsync<Produto>(id);
            if (produto is null)
                return null;

            produto.Ativo = 0;
            produto.Data_alteracao = DateTime.Now;
            var update = connection.Update<Produto>(produto);
            if (!update)
                return null;

            return produto;
        }

        public async Task<List<Produto>> FiltrarPorDescricao(string descricao)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produtos = connection.Query<Produto, Setor, Produto>(
                            sql: ProdutoQuerys.SELECT_POR_DESCRICAO,
                            map: (produto, setor) =>
                            {
                                produto.Setor = setor;
                                return produto;
                            },
                            param: new { descricao }).ToList();
            if (produtos.Any())
                return produtos;

            return null;
        }

        public async Task<List<Produto>> FiltrarPorSetor(string setor)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produtos = connection.Query<Produto, Setor, Produto>(
                            sql: ProdutoQuerys.SELECT_POR_SETOR,
                            map: (produto, setor) =>
                            {
                                produto.Setor = setor;
                                return produto;
                            },
                            param: new { setor }).ToList();
            if (produtos.Any())
                return produtos;

            return null;
        }

        public Task<List<Produto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> GetByIdAsync(int produto_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var produto = connection.Query<Produto, Setor, Produto>(
                        sql: ProdutoQuerys.SELECT_POR_ID,
                        map: (produto, setor) =>
                        {
                            produto.Setor = setor;
                            return produto;
                        },
                        param: new { produto_id }).FirstOrDefault();
            if (produto is null)
                return null;

            return produto;
        }

        public async Task<List<Produto>> GetByKeyAsync(string codigo)
        {
            using var connection = await _connection.GetConnectionAsync();
            List<Produto> ret = new();
            var produtos = connection.Query<Produto, Setor, Produto>(
                                sql: ProdutoQuerys.SELECT_POR_CODIGO,
                                map: (produto, setor) =>
                                {
                                    produto.Setor = setor;
                                    return produto;
                                },
                                param: new { codigo }).ToList();
            if (produtos.Any())
                return produtos;

            return null;
        }

        public async Task<List<Setor>> ObterSetores()
        {
            using var connection = await _connection.GetConnectionAsync();
            var setores = await connection.QueryAsync<Setor>(sql: ProdutoQuerys.SELECT_LISTAR_SETORES);
            if (setores is null)
                return null;
            return setores.ToList();
        }

        public async Task<ObterProdutoListagemSetoresResponse> ObterProdutoListagemSetores(int produto_id)
        {
            ObterProdutoListagemSetoresResponse ret = new();
            using var connection = await _connection.GetConnectionAsync();
            var produto = connection.Query<Produto, Setor, Produto>(
                        sql: ProdutoQuerys.SELECT_POR_ID,
                        map: (produto, setor) =>
                        {
                            produto.Setor = setor;
                            return produto;
                        },
                        param: new { produto_id }).FirstOrDefault();
            if (produto is null)
                return null;

            var setores = await connection.QueryAsync<Setor>(
                sql: ProdutoQuerys.SELECT_LISTAR_SETORES);

            if (setores is null)
                return null;

            ret.Produto = produto;
            ret.Setores = setores.ToList();
            return ret;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            using var connection = await _connection.GetConnectionAsync();
            produto.Setor_id = await connection.QueryFirstAsync<int>(
                            ProdutoQuerys.SELECT_ID_SETOR,
                            new { descricao = produto.Setor.Descricao });

            var produtoBanco = await connection.GetAsync<Produto>(produto.Id);
            if (produtoBanco is not null)
            {
                produtoBanco.Descricao = produto.Descricao;
                produtoBanco.Preco_custo = double.Parse(produto.Preco_custo.ToString().Replace(".", ","));
                produtoBanco.Preco_venda = double.Parse(produto.Preco_venda.ToString().Replace(".", ","));
                produtoBanco.Setor_id = produto.Setor_id;
                produtoBanco.Ativo = 1;
                produtoBanco.Data_alteracao = DateTime.Now;

                var update = await connection.UpdateAsync<Produto>(produtoBanco);
                if (!update)
                    return null;

                return await GetProdutoAsync(produtoBanco.Id, connection);
            }
            return null;
        }

        public async Task<int> ObterSetorId(string setor)
        {
            using var connection = await _connection.GetConnectionAsync();
            return connection.QueryFirst<int>(
                            ProdutoQuerys.SELECT_ID_SETOR,
                            new { descricao = setor });
        }

        public async Task<List<FornecedorDescricaoId>> ObterFornecedorDescricaoId(int produto_id, MySqlConnection connection = null)
        {
            IEnumerable<FornecedorDescricaoId> fornecedorBanco;
            if (connection is null)
            {
                using var conn = await _connection.GetConnectionAsync();
                fornecedorBanco = await conn.QueryAsync<FornecedorDescricaoId>(
                    sql: ProdutoQuerys.SELECT_OBTER_FORNECEDOR_POR_ID_PRODUTO,
                    param: new { produto_id });

                if (fornecedorBanco.Any())
                    return fornecedorBanco.ToList();

                return null;
            }
            fornecedorBanco = await connection.QueryAsync<FornecedorDescricaoId>(
                    sql: ProdutoQuerys.SELECT_OBTER_FORNECEDOR_POR_ID_PRODUTO,
                    param: new { produto_id });

            if (fornecedorBanco.Any())
                return fornecedorBanco.ToList();

            return null;
        }

        public async Task<Fornecedor> ObterFornecedorDetalhes(int fornecedor_id)
        {
            using var connection = await _connection.GetConnectionAsync();
            var fornecedor = await _fornecedorRepository.GetFornecedorAsync(fornecedor_id, connection);
            if (fornecedor is null)
                return null;

            return fornecedor;
        }

        public async Task<ObterFornecedoresDadosProdutoResponse> ObterListagemFornecedoresDadosProduto(int produto_id)
        {
            ObterFornecedoresDadosProdutoResponse ret = new();
            List<ListaFornecedorResponse> fornecedoresResponse;

            using var conn = await _connection.GetConnectionAsync();
            fornecedoresResponse = (await conn.QueryAsync<ListaFornecedorResponse>(
                sql: ProdutoQuerys.SELECT_LISTA_FORNECEDORES)).ToList();

            var produto = await conn.QueryFirstOrDefaultAsync<DadosProdutoResponse>(
                sql: ProdutoQuerys.SELECT_ID_COD_DESC_PRODUTO,
                param: new { produto_id });

            if(produto is not null)
            {
                ret.IdProduto = produto.Id;
                ret.CodigoProduto = produto.Codigo;
                ret.DescircaoProduto = produto.Descricao;
            }
            if (fornecedoresResponse.Any())
            {
                foreach (var fornecedor in fornecedoresResponse)
                {
                    ret.Fornecedores.Add(new Fornecedor()
                    {
                        Id = fornecedor.Id,
                        Cnpj = fornecedor.Cnpj,
                        Nome_empresa = $"{fornecedor.Cnpj} - {fornecedor.Nome_empresa}"
                    });
                }
            }
            return ret;
        }

        public async Task<Produto> InserirFornecedorProduto(int produtoId, string fornecedorDescricao)
        {
            var cnpj = fornecedorDescricao.Split("-")[0].Trim();
            using var connection = await _connection.GetConnectionAsync();
            var fornecedorId = await connection.QueryFirstOrDefaultAsync<int>(
                sql: ProdutoQuerys.SELECT_FORNECEDOR_POR_CNPJ,
                param: new { cnpj });

            var fornecedorJaCadastrado = await connection.QueryFirstOrDefaultAsync<FornecedorProduto>(
                sql: ProdutoQuerys.SELECT_FORNECEDOR_PRODUTO,
                param: new { fornecedorId, produtoId });

            if (fornecedorJaCadastrado is not null)
            {
                fornecedorJaCadastrado.Ativo = 1;
                fornecedorJaCadastrado.Data_alteracao = DateTime.Now;
                var update = await connection.UpdateAsync<FornecedorProduto>(fornecedorJaCadastrado);
                if (!update)
                    return null;
            }
            else
            {
                var insert = await connection.InsertAsync<FornecedorProduto>(new FornecedorProduto()
                {
                    Fornecedor_id = fornecedorId,
                    Produto_id = produtoId,
                    Ativo = 1,
                    Data_criacao = DateTime.Now,
                    Data_alteracao = DateTime.Now
                });
                if (insert <= 0)
                    return null;
            }
            return await GetProdutoAsync(produtoId, connection);
        }

        public async Task<List<FornecedorDescricaoId>> ExcluirFornecedor(int fornecedorId, int produtoId)
        {
            using var connection = await _connection.GetConnectionAsync();
            var fornecedorBanco = await connection.QueryFirstOrDefaultAsync<FornecedorProduto>(
                sql: ProdutoQuerys.SELECT_FORNECEDOR_PRODUTO,
                param: new { fornecedorId, produtoId });

            fornecedorBanco.Ativo = 0;
            fornecedorBanco.Data_alteracao = DateTime.Now;

            var update = await connection.UpdateAsync<FornecedorProduto>(fornecedorBanco);
            if (!update)
                return null;

            return await ObterFornecedorDescricaoId(produtoId, connection);
        }

        public async Task<Produto> GetProdutoAsync(int produto_id, MySqlConnection connection)
        {
            var produto = connection.Query<Produto, Setor, Produto>(
                             sql: ProdutoQuerys.SELECT_POR_ID,
                             (produto, setor) =>
                             {
                                 produto.Setor = setor;
                                 return produto;
                             },
                             param: new { produto_id }).FirstOrDefault();
            if (produto is null)
                return null;

            return produto;
        }
    }
}
