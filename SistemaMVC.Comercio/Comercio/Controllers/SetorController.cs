using Comercio.Exceptions.Setor;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    [Route("[controller]")]
    public class SetorController : Controller
    {
        private readonly ISetorService _service;
        private readonly ISetorAdapter _mapper;

        public SetorController(ISetorService service, ISetorAdapter mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var setoresBanco = await _service.ListarSetores();
                var setoresView = _mapper.MontaListaSetorViewModel(setoresBanco);
                return View(setoresView);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("editar/{id}")]
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

        [HttpPost]
        [Route("inserir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir([Required][MaxLength(30)] string descricao)
        {
            try
            {
                var setorResponse = await _service.Inserir(descricao);
                if (setorResponse is null)
                    return View("Error", new ErrorViewModel().SetorErroAoTentarInserir());

                var setoresViewModel = _mapper.MontaListaSetorViewModel(await _service.ListarSetores());
                return View("Index", setoresViewModel);
            }
            catch (DescricaoInvalidaException)
            {
                return View("Error", new ErrorViewModel().SetorErroInserirDescricaoInvalida());
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar([Bind("Id, Descricao")] SetorViewModel setor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var setorResponse = await _service.AtualizarSetor(setor);
                    if (setorResponse is null)
                        return View("Error", new ErrorViewModel().SetorErroAoTentarAtualizar());

                    var setoresViewModel = _mapper.MontaListaSetorViewModel(await _service.ListarSetores());
                    return View("Index", setoresViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao());
            }
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var delete = await _service.ExcluirSetor(id);
                if (!delete)
                    return View("Error", new ErrorViewModel().SetorErroAoTentarExcluir());

                var setoresViewModel = _mapper.MontaListaSetorViewModel(await _service.ListarSetores());
                return View("Index", setoresViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
    }
}
