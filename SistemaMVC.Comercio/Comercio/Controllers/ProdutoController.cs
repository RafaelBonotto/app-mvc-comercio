using Comercio.Exceptions.Produto;
using Comercio.Extensions;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoAdapter _mapper;
        private readonly IFornecedorAdapter _mapperFornecedor;
       
        public ProdutoController(
            IProdutoService produtoService, 
            IProdutoAdapter adaper, 
            IFornecedorAdapter mapperFornecedor)
        {
            _produtoService = produtoService;
            _mapper = adaper;
            _mapperFornecedor = mapperFornecedor;
        }

        public IActionResult Index() => View();

        [Route("[controller]/filtro")]
        public async Task<IActionResult> CarregaSetoresExibeViewFiltro()
        {
            try
            {
                var setores = new SelectList(await _produtoService.ListarSetores());
                return View("Filtro", setores);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/inserir")]
        public async Task<IActionResult> CarregaSetoresExibeViewInserir()
        {
            try
            {
                ProdutoViewModel produtoViewModel = new();
                var setores = new SelectList(await _produtoService.ListarSetores());
                produtoViewModel.SetoresBanco = setores;
                return View("Inserir", produtoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/filtrarPorCodigo")]
        public async Task<IActionResult> FiltrarPorCodigo(string codigo)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorCodigo(codigo);
                if (produtos.Count == 0)
                    return View("Error", new ErrorViewModel().ErroFiltroNaoEncontrado());

                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));

                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/filtrarPorDescricao")]
        public async Task<IActionResult> FiltrarPorDescricao(string descricao)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorDescricao(descricao);
                if(produtos.Count == 0)
                    return View("Error", new ErrorViewModel().ErroFiltroNaoEncontrado());

                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));
                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/filtrarPorSetor")]
        public async Task<IActionResult> FiltrarPorSetor(string setor)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorSetor(setor);
                if(produtos.Count == 0)
                    return View("Error", new ErrorViewModel().ErroFiltroNaoEncontrado());

                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));

                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);
                if (produto is null)
                    return View("Error", new ErrorViewModel().ErroAoCarregarDetalhes());

                var produtoViewModel = _mapper.MontaProdutoViewModel(produto);
                return View("Detalhes", produtoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);
                if (produto is null)
                    return View("Error", new ErrorViewModel().ErroAoCarregarDetalhes());

                var produtoViewModel = _mapper.MontaProdutoViewModel(produto);
                var setores = new SelectList(await _produtoService.ListarSetores()); // AQUI EXCLUIR (CARREGA SETORES NO REPOSITORY)
                if(setores is null)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());

                produtoViewModel.SetoresBanco = setores;
                return View("Editar", produtoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("[controller]/adicionar/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var produtoResponse = await _produtoService.InserirProduto(produto);
                    if (produtoResponse is null)
                        return View("Error", new ErrorViewModel().ProdutoErroAoTentarInserir());

                    var produtoViewModel = _mapper.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (CodigoInvalidoException)
                {
                    return View("Error", new ErrorViewModel().ProdutoErroCodigoInvalido());
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }                
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpPost]
        [Route("[controller]/adicionarFornecedor/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarFornecedor(int produtoId, string fornecedorDescricao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produtoResponse = await _produtoService.InserirFornecedorProduto(produtoId, fornecedorDescricao);
                    if (produtoResponse is null)
                        return View("Error", new ErrorViewModel().ProdutoErroAoTentarInserirFornecedor());

                    var produtoViewModel = _mapper.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpPost]
        [Route("[controller]/atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produtoResponse = await _produtoService.AtualizarProduto(produto);
                    if (produtoResponse is null)
                        return View("Error", new ErrorViewModel().ProdutoErroAoTentarAtualizar());

                    var produtoViewModel = _mapper.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }


        [Route("[controller]/excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var delete = await _produtoService.ExcluirProduto(id);
                if (!delete)
                    return View("Error", new ErrorViewModel().ProdutoErroAoTentarExcluir());

                return View("Index");
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpGet]
        [Route("[controller]/obterFornecedor")]
        public async Task<IActionResult> ObterFornecedor(int produtoId)
        {
            try
            {
                var fornecedores = await _produtoService.ObterFornecedor(produtoId);
                if (fornecedores is null || fornecedores.Count == 0)
                    return View("Error", new ErrorViewModel().ProdutoFornecedorNaoEncontrado());

                var listaViewModel = new List<ListarFornecedorViewModel>();
                foreach (var fornecedor in fornecedores)
                    listaViewModel.Add(_mapper.CriarListaFornecedorViewModel(fornecedor, produtoId));

                return View("ExibirFornecedor", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpGet]
        [Route("[controller]/obterFornecedorDetalhes")]
        public async Task<IActionResult> ObterFornecedordetalhes(int fornecedorId, int produtoId)
        {
            try
            {
                var fornecedor = await _produtoService.ObterFornecedorDetalhes(fornecedorId);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().ProdutoFornecedorNaoEncontrado());

                var viewModel = _mapper.CriarListaFornecedorViewModel(fornecedor, produtoId);
                return View("ExibirFornecedorDetalhes", viewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/excluirFornecedor")]
        public async Task<IActionResult> ExcluirFornecedor(int fornecedorId, int produtoId)
        {
            try
            {
                var fornecedores = await _produtoService.ExcluirFornecedor(fornecedorId, produtoId);
                if (fornecedores is null)
                    return View("Error", new ErrorViewModel().ProdutoFornecedorNaoEncontrado());

                var listaViewModel = new List<ListarFornecedorViewModel>();
                foreach (var fornecedor in fornecedores)
                    listaViewModel.Add(_mapper.CriarListaFornecedorViewModel(fornecedor, produtoId));

                return View("ExibirFornecedor", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
    }
}
