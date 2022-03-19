using Comercio.Exceptions.Produto;
using Comercio.Interfaces.ProdutoInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoAdapter _mapper;
        private const string MSG_ERRO_FILTRO = "Não foram encontrados produtos para esse filtro.";
        private const string MSG_ERRO_INSERIR = "Algo deu errado ao tentar inserir o produto.";
        private const string MSG_ERRO_CODIGO_INVALIDO = "Impossível inserir o produto, foi encontrado um produto ativo com o mesmo código.";
        private const string MSG_ERRO_PAGINA = "Algo deu errado ao tentar carregar essa página.";
        private const string MSG_ERRO_DETALHES_PRODUTO = "Algo deu errado ao tentar carregar detalhes do produto.";
        public ProdutoController(IProdutoService produtoService, IProdutoAdapter adaper)
        {
            _produtoService = produtoService;
            _mapper = adaper;
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
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
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
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }

        [Route("[controller]/filtrarPorCodigo")]
        public async Task<IActionResult> FiltrarPorCodigo(string codigo)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorCodigo(codigo);

                if (produtos.Count == 0)
                    return View("Error", new ErrorViewModel(MSG_ERRO_FILTRO));

                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));

                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
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
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));

                if (produtos.Count == 0)
                    return View("Error", new ErrorViewModel(MSG_ERRO_FILTRO));

                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }

        [Route("[controller]/filtrarPorSetor")]
        public async Task<IActionResult> FiltrarPorSetor(string setor)
        {
            try
            {
                var produtos = await _produtoService.FiltrarPorSetor(setor);
                var listaViewModel = new List<ProdutoViewModel>();
                foreach (var produto in produtos)
                    listaViewModel.Add(_mapper.MontaProdutoViewModel(produto));

                if (produtos.Count == 0)
                    return View("Error", new ErrorViewModel(MSG_ERRO_FILTRO));

                return View("Produtos", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }

        [Route("[controller]/detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);
                if (produto is null)
                    return View("Error", new ErrorViewModel(MSG_ERRO_DETALHES_PRODUTO));

                var produtoViewModel = _mapper.MontaProdutoViewModel(produto);
                return View("Detalhes", produtoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }

        [Route("[controller]/editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var produto = await _produtoService.DetalhesProduto(id);                
                if (produto is null)
                    return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));

                var produtoViewModel = _mapper.MontaProdutoViewModel(produto);
                var setores = new SelectList(await _produtoService.ListarSetores());
                produtoViewModel.SetoresBanco = setores;
                return View("Editar", produtoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }

        [HttpPost]
        [Route("[controller]/adicionar/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(
            [Bind("Id, Codigo, Descricao, SetorDescricao, Preco_custo, Preco_venda")]
            ProdutoViewModel produto)//]VALIDAÇÃO DO CAMPOS STRINGS QUE VÃO SE TORNAR DOUBLE
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var produtoResponse = await _produtoService.InserirProduto(produto);
                    if (produtoResponse is null)
                        return View("Error", new ErrorViewModel(MSG_ERRO_INSERIR));

                    var produtoViewModel = _mapper.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (CodigoInvalidoException)
                {
                    return View("Error", new ErrorViewModel(MSG_ERRO_CODIGO_INVALIDO));
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
                }                
            }
            else
            {
                return NotFound($"Erro de validação -  { ModelState.Values }");
            }
        }

        [HttpPost]
        [Route("[controller]/atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(
            [Bind("Id, Codigo, Descricao, SetorDescricao, Preco_custo, Preco_venda")]
            ProdutoViewModel produto)//]VALIDAÇÃO DO CAMPOS STRINGS QUE VÃO SE TORNAR DOUBLE
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var produtoResponse = await _produtoService.AtualizarProduto(produto);
                    if (produtoResponse is null)
                        return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));

                    var produtoViewModel = _mapper.MontaProdutoViewModel(produtoResponse);
                    return View("Detalhes", produtoViewModel);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }


        [Route("[controller]/excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var delete = await _produtoService.ExcluirProduto(id);
                if (!delete)
                    return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));

                return View("Index");
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel(MSG_ERRO_PAGINA));
            }
        }
    }
}
