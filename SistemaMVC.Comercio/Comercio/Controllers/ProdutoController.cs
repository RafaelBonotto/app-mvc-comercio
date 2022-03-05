﻿using Comercio.Interfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class ProdutoController : Controller
    {
        public IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public IActionResult Index()=> View();
        public IActionResult Filtro()=> View();
        public IActionResult Novo()=> View();

        [Route("[controller]/filtrarPorCodigo")]
        public async Task<IActionResult> FiltrarPorCodigo(string codigo)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorCodigo(codigo);

                if (produtos.Count == 0) 
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View("Produtos", produtos);
            }
            catch (System.Exception error)
            {
                return NotFound(error.Message);
            }
        }

        [Route("[controller]/filtrarPorDescricao")]
        public async Task<IActionResult> FiltrarPorDescricao(string descricao)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorDescricao(descricao);

                if (produtos.Count == 0)
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View("Produtos", produtos);
            }
            catch (System.Exception error)
            {
                return NotFound(error.Message);
            }
        }

        [Route("[controller]/filtrarPorSetor")]
        public async Task<IActionResult> FiltrarPorSetor(int setor)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorSetor(setor);

                if (produtos.Count == 0)
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View("Produtos", produtos);
            }
            catch (System.Exception error)
            {
                return NotFound(error.Message);
            }
        }

        [Route("[controller]/detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);

                if (produto is null) 
                    return NotFound("Nenhum produto encontrado");

                return View(produto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("[controller]/editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);

                if (produto is null) return NotFound("Produto não encontrado no sistema");

                return View(produto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[controller]/atualizar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar([Bind
            ("Codigo, Descricao, Setor, Preco_custo, Preco_venda")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _produtoService.AtualizarProduto(produto);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                return View(produto);
            }
        }

        [Route("[controller]/excluir/{produtoId}")]
        public async Task<IActionResult> Excluir(int produtoId)
        {
            try
            {
                var delete = await _produtoService.ExcluirProduto(produtoId);

                if (!delete) return NotFound("Erro ao excluir o produto");

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
