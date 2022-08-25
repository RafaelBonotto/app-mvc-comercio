using Comercio.Entities;
using Comercio.Exceptions.Produto;
using Comercio.Extensions;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using Comercio.Responses.Produto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoAdapter _mapper;
        private readonly IFornecedorAdapter _mapperFornecedor;
        private readonly IProdutoRepository _repositoryProduto;
        private readonly IRepositoryBase<Produto> _repositoryBase;
       
        public ProdutoController(
            IProdutoAdapter adaper, 
            IFornecedorAdapter mapperFornecedor,
            IProdutoRepository repositoryProduto,
            IRepositoryBase<Produto> repositoryBase)
        {
            _mapper = adaper;
            _mapperFornecedor = mapperFornecedor;
            _repositoryProduto = repositoryProduto;
            _repositoryBase = repositoryBase;
        }

        public IActionResult Index() => View();

        [Route("[controller]/filtro")]
        public async Task<IActionResult> CarregaSetoresExibeViewFiltro()
        {
            try
            {
                var setores = await _repositoryProduto.ObterSetores();
                if(setores is null)
                    return View("Error", new ErrorViewModel().ProdutoErroAoCarregarSetores());

                return View("Filtro", new SelectList(setores));
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
                var setores = await _repositoryProduto.ObterSetores();
                if (setores is null)
                    return View("Error", new ErrorViewModel().ProdutoErroAoCarregarSetores());

                produtoViewModel.SetoresBanco = new SelectList(setores);
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
                var produtos =  await _repositoryBase.GetByKeyAsync(codigo);
                if (produtos is null || produtos.Count == 0)
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
                var produtos = await _repositoryProduto.FiltrarPorDescricao(descricao);
                if(produtos is null)
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
                var produtos = await _repositoryProduto.FiltrarPorSetor(setor);
                if(produtos is null)
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
        public async Task<IActionResult> 
            Detalhes(int id)
        {
            try
            {
                var produto = await _repositoryBase.GetByIdAsync(id);
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

        [Route("[controller]/viewDetalhes")]
        public async Task<IActionResult> ViewDetalhes(ProdutoViewModel produto)
        {
            try
            {
                if (produto is null)
                    return View("Error", new ErrorViewModel().ErroAoCarregarDetalhes());

                return View("Detalhes", produto);
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
                var produto = await _repositoryBase.GetByIdAsync(id);
                if (produto is null)
                    return View("Error", new ErrorViewModel().ErroAoCarregarDetalhes());

                var produtoViewModel = _mapper.MontaProdutoViewModel(produto);
                var setores = new SelectList(await _repositoryProduto.ObterSetores()); // AQUI EXCLUIR (CARREGA SETORES NO get by id da REPOSITORY)
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
                    var produtoRepository = _mapper.MontaProdutoInsertRepositorio(produto);
                    var produtoResponse = await _repositoryBase.AddAsync(produtoRepository);
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
                    var produtoResponse = await _repositoryProduto.InserirFornecedorProduto(produtoId, fornecedorDescricao);
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

        [Route("[controller]/adicionarFornecedorView")]
        public async Task<IActionResult> AdicionarFornecedorView(int produto_id)// produto_id e busca cod e descricao p/ exibir a view junto c/ listta de fornecedores
        {
            try
            {
                var response = await _repositoryProduto.ObterTodosFornecedoresEDadosDoProduto(produto_id);// Criar metodo que retorna tds Fornecedores + id, cod, desc do produto
                if (response is null)
                    return View("Error", new ErrorViewModel().ProdutoErroAoCarregarFornecedores());

                var viewModel = _mapper.MontaAdicionarFornecedorViewModel(response);
                return View("AdicionarFornecedor", viewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
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
                    var produtoRepositorio = _mapper.MontaProdutoUpdateRepositorio(produto);
                    var produtoResponse = await _repositoryBase.UpdateAsync(produtoRepositorio);
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
                Produto response = await _repositoryBase.DeleteAsync(id);
                if(response is null)
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
                var viewModel = new ObterFornecedorViewModel();
                var fornecedores = await _repositoryProduto.ObterFornecedorDescricaoId(produtoId);
                if (fornecedores is null || fornecedores.Count == 0)
                    return View("ExibirFornecedor", viewModel);

                viewModel.Produto_id = produtoId;
                viewModel.Fornecedores = fornecedores;
                return View("ExibirFornecedor", viewModel);
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
                var fornecedor = await _repositoryProduto.ObterFornecedorDetalhes(fornecedorId);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().ProdutoFornecedorNaoEncontrado());

                var viewModel = _mapper.CriarObterFornecedorDetalhesViewModel(fornecedor, produtoId);
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
                var fornecedores = await _repositoryProduto.ExcluirFornecedor(fornecedorId, produtoId);
                if (fornecedores is null)
                    return View("Error", new ErrorViewModel().ProdutoFornecedorNaoEncontrado());

                ObterFornecedorViewModel viewModel = new()
                {
                    Produto_id = produtoId,
                    Fornecedores = fornecedores
                };
                return View("ExibirFornecedor", viewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
    }
}
