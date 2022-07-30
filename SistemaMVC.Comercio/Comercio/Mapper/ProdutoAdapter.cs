﻿using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comercio.Mapper
{
    public class ProdutoAdapter : IProdutoAdapter
    {
        public ListarFornecedorViewModel CriarListaFornecedorViewModel(Fornecedor fornecedor, int produto_id)
        {
            var ret = new ListarFornecedorViewModel
            {
                Id = fornecedor.Id,
                Produto_id = produto_id,
                Cnpj = fornecedor.Cnpj,
                Email = fornecedor.Email,
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Telefone = fornecedor.Telefone,
                Endereco = fornecedor.Endereco,
                Vendedor = fornecedor.Vendedor
            };
            ret.TipoTelefone = new SelectList(fornecedor.DescricaoTipoTelefone);
            ret.TipoEndereco = new SelectList(fornecedor.DescricaoTipoEndereco);
            return ret;
        }

        public Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio)
        {
            produtoRepositorio.Descricao = produtoViewModel.Descricao;
            produtoRepositorio.Preco_custo = double.Parse(produtoViewModel.Preco_custo.Replace(".", ","));
            produtoRepositorio.Preco_venda = double.Parse(produtoViewModel.Preco_venda.Replace(".", ","));
            produtoRepositorio.Setor_id = produtoViewModel.Setor_id;
            produtoRepositorio.Ativo = 1;
            produtoRepositorio.Data_alteracao = DateTime.Now;
            return produtoRepositorio;
        }

        public Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel)
        {
            Produto produto = new()
            {
                Codigo = produtoViewModel.Codigo,
                Descricao = produtoViewModel.Descricao.ToUpper(),
                Preco_custo = double.Parse(produtoViewModel.Preco_custo.Replace(".", ",")),
                Preco_venda = double.Parse(produtoViewModel.Preco_venda.Replace(".", ",")),
                Setor_id = produtoViewModel.Setor_id,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
            produto.Setor.Descricao = produtoViewModel.SetorDescricao;
            return produto;
        }

        public ProdutoViewModel MontaProdutoViewModel(Produto produto)
        {
            return new ProdutoViewModel()
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao.ToUpper(),
                Preco_custo = "Não Registrado",//produto.Preco_custo.ToString("N2"),
                Preco_venda = produto.Preco_venda.ToString("N2"),
                Ativo = produto.Ativo == 0 ? "INATIVO " : "ATIVO",
                SetorDescricao = produto.Setor.Descricao.ToUpper(),
                FornecedoresBanco = new SelectList(produto.Fornecedores)
            };
        }

        public List<FornecedorViewModel> MontaListaFornecedorViewModel(List<Fornecedor> fornecedores)
        {
            List<FornecedorViewModel> ret = new();
            foreach (var fornecedor in fornecedores)
            {
                ret.Add(new FornecedorViewModel()
                {
                    Nome_empresa = fornecedor.Nome_empresa
                });
            }
            return ret;
        }
    }
}
