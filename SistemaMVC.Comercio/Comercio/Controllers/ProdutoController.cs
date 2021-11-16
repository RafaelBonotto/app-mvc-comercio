using Comercio.Data.Context;
using Comercio.Data.Repositories;
using Comercio.Interfaces.Base;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class ProdutoController : Controller
    {
        public IRepositoryBase<Produto> _context;

        public ProdutoController(IRepositoryBase<Produto> context)
        {
            _context = context;
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
                //var produtos = await _produtoService.FiltrarProdutos(codigo, descricao, setor);
                var produtosBanco = await _context.GetAllAsync();
                var produtos = produtosBanco.Where(x => x.Codigo == codigo || x.Descricao == descricao || x.Setor == setor).ToList();
                if (produtos is null) return NotFound("Nenhum produto encontrado no sistema para esse filtro");

                return View(produtos);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
