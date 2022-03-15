﻿using Comercio.Entities;
using Comercio.Enum;
using Comercio.Interfaces;
using Comercio.Models;
using System;

namespace Comercio.Mapper
{
    public class Adapter : IAdapter
    {
        public Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio)
        {
            produtoRepositorio.Descricao = produtoViewModel.Descricao;
            produtoRepositorio.Preco_custo = double.Parse(produtoViewModel.Preco_custo.Replace(".", ","));
            produtoRepositorio.Preco_venda = double.Parse(produtoViewModel.Preco_venda.Replace(".", ","));
            produtoRepositorio.Setor_id = produtoViewModel.Setor.GetHashCode();
            produtoRepositorio.Ativo = 1;
            produtoRepositorio.Data_alteracao = DateTime.Now;
            return produtoRepositorio;
        }

        public Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel)
        {
            return new Produto()
            {
                Descricao = produtoViewModel.Descricao,
                Preco_custo = double.Parse(produtoViewModel.Preco_custo.Replace(".", ",")),
                Preco_venda = double.Parse(produtoViewModel.Preco_venda.Replace(".", ",")),
                Setor_id = produtoViewModel.Setor.GetHashCode(),
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public ProdutoViewModel MontaProdutoViewModel(Produto produto)
        {
            var aux = new ProdutoViewModel()
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                Preco_custo = produto.Preco_custo.ToString("N2"),
                Preco_venda = produto.Preco_venda.ToString("N2"),
                Ativo = produto.Ativo == 0 ? "Inativo " : "Ativo",
                Setor = (Setores)produto.Setor_id
            };
            return aux;
        }
    }
}
