using Comercio.Entities;
using Comercio.Interfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;

namespace Comercio.Mapper
{
    public class ProdutoAdapter : IProdutoAdapter
    {
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
            return new Produto()
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
        }

        public ProdutoViewModel MontaProdutoViewModel(Produto produto)
        {
            return new ProdutoViewModel()
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao.ToUpper(),
                Preco_custo = produto.Preco_custo.ToString("N2"),
                Preco_venda = produto.Preco_venda.ToString("N2"),
                Ativo = produto.Ativo == 0 ? "INATIVO " : "ATIVO",
                SetorDescricao = produto.Setor.Descricao.ToUpper()
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
