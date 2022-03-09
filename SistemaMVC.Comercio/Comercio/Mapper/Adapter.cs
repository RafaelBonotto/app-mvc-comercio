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
    }
}
