using Comercio.Interfaces;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Filtro()
        {
            return View();
        }

        [Route("[controller]/filtrar")]
        public async Task<IActionResult> Produtos(string codigo, string descricao, string setor)
        {
            try
            {
                var produtos = await _produtoService.FiltrarProdutos(codigo, descricao, setor);

                if (produtos.Count == 0) 
                    return NotFound("Não foram encontrados produtos para esse filtro");

                return View(produtos);
            }
            catch (System.Exception error)
            {
                return NotFound(error.Message);
            }
        }

        [Route("[controller]/Detalhes/{id}")]
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
    }
}
