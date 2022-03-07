using Comercio.Models;
using System;

namespace Comercio.Mapper
{
    public static class Adapter
    {
        public static Produto MontaProdutoUpdateRepositorio(Produto produtoRequest, Produto produtoRepositorio)
        {
            produtoRepositorio.Descricao = produtoRequest.Descricao;
            produtoRepositorio.Preco_custo = produtoRequest.Preco_custo;
            produtoRepositorio.Preco_venda = produtoRequest.Preco_venda;
            produtoRepositorio.Setor_id = produtoRequest.Setor_id;
            produtoRepositorio.Ativo = 1;
            produtoRepositorio.Data_alteracao = DateTime.Now;
            return produtoRepositorio;
        }
    }
}
