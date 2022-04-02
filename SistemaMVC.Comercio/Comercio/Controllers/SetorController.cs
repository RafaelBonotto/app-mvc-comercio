using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class SetorController : Controller
    {
        private readonly ISetorService _service;
        private readonly ISetorAdapter _mapper;

        public SetorController(ISetorService service, ISetorAdapter mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var setorBanco = await _service.ObterSetor(id);
                var setorViewModel = _mapper.MontaSetorViewModel(setorBanco);
                return View("Editar", setorViewModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var setoresBanco =  await _service.ListarSetores();
                var setoresView = _mapper.MontaListaSetorViewModel(setoresBanco);
                return View(setoresView);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
