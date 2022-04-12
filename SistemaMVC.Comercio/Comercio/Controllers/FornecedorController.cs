using Comercio.Interfaces.FornecedorInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _service;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorController(IFornecedorService fornecedorService, IFornecedorAdapter adaper)
        {
            _service = fornecedorService;
            _mapper = adaper;
        }

        public IActionResult Index() => View();

        [Route("[controller]/listar-fornecedores")]
        public async Task<IActionResult> ListarFornecedores()
        {
            try
            {
                List<Comercio.Models.FornecedorViewModel> ret = new();
                return View("Fornecedores", ret);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
