using Microsoft.AspNetCore.Mvc;

namespace Comercio.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _service;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorController(IFornecedorService fornecedorService, IFornecedorAdapter adaper)
        {
            _fornecedorService = fornecedorService;
            _mapper = adaper;
        }

        public IActionResult Index() => View();
    }
}
