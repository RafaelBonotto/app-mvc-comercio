using Comercio.Interfaces;
using Comercio.Mapper;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult Index() => View();
        public IActionResult Filtro() => View();
        public IActionResult Novo() => View();

        [Route("[controller]/filtrarPorCodigo")]
        public async Task<IActionResult> FiltrarPorCodigo(string codigo)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorCodigo(codigo);
                if (produtos.Count == 0)
                    return NotFound("Não foram encontrados produtos para esse filtro");

                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(Adapter.MontaProdutoViewModel(produto));

                return View("Produtos", listaViewModel);
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
                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(Adapter.MontaProdutoViewModel(produto));

                if (produtos.Count == 0)
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View("Produtos", listaViewModel);
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
                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(Adapter.MontaProdutoViewModel(produto));

                if (produtos.Count == 0)
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View("Produtos", listaViewModel);
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

                var produtoViewModel = Adapter.MontaProdutoViewModel(produto);
                return View("Detalhes", produtoViewModel);
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

                if (produto is null) 
                    return NotFound("Produto não encontrado no sistema");

                var produtoViewModel = Adapter.MontaProdutoViewModel(produto);
                return View("Editar", produtoViewModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[controller]/atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar([Bind("Id, Codigo, Descricao, Setor_id, Preco_custo, Preco_venda")] ProdutoViewModel produto)//]VALIDAÇÃO DO CAMPOS STRINGS QUE VÃO SE TORNAR DOUBLE
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produtoResponse = await _produtoService.AtualizarProduto(produto);
                    if (produtoResponse is null)
                        return NotFound("Erro ao tentar atualizar o produto"); 
                    
                    var produtoViewModel = Adapter.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                return NotFound($"Erro de validação -  { ModelState.Values }");
            }
        }


        [Route("[controller]/excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var delete = await _produtoService.ExcluirProduto(id);

                if (!delete) 
                    return NotFound("Erro ao excluir o produto");

                return View("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
