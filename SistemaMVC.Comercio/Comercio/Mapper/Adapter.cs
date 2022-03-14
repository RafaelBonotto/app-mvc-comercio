using Comercio.Entities;
using Comercio.Models;
using System;

namespace Comercio.Mapper
{
    public static class Adapter
    {
        public static Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio)
        {
            produtoRepositorio.Descricao = produtoViewModel.Descricao;
            produtoRepositorio.Preco_custo = double.Parse(produtoViewModel.Preco_custo.Replace(".", ","));
            produtoRepositorio.Preco_venda = double.Parse(produtoViewModel.Preco_venda.Replace(".", ","));
            produtoRepositorio.Setor_id = produtoViewModel.Setor_id;
            produtoRepositorio.Ativo = 1;
            produtoRepositorio.Data_alteracao = DateTime.Now;
            return produtoRepositorio;
        }

        public static ProdutoViewModel MontaProdutoViewModel(Produto produto)
        {
            return new ProdutoViewModel()
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                Preco_custo = produto.Preco_custo.ToString("N2"),
                Preco_venda = produto.Preco_venda.ToString("N2"),
                Ativo = produto.Ativo == 0 ? "Inativo " : "Ativo",
                Setor = produto.Setor.Descricao
            };
        }
    }
}
