using Comercio.Entities;
using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Inserir() => View();

        [HttpPost]
        [Route("[controller]/adicionar/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(
            [Bind("Cnpj, Nome_empresa")] 
            FornecedorViewModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorResponse = await _service.InserirFornecedor(fornecedor);
                    if (fornecedorResponse is null)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // CRIAR ERRO PARA O FORNECEDOR

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedorResponse);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (CnpjInvalidoException)
                {
                    return View("Error", new ErrorViewModel().FornecedorErroInserirCnpjInvalido());
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

        [HttpPost]
        [Route("[controller]/adicionar-telefone/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarTelefone(int fornecedorId, string ddd, string numero, string tipoTelefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorResponse = await _service.InserirTelefone(fornecedorId, ddd, numero, tipoTelefone);
                    if (fornecedorResponse is null)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedorResponse);
                    return View("Detalhes", fornecedorViewModel);
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

        [HttpPost]
        [Route("[controller]/excluir-telefone/")]
        public async Task<IActionResult> ExcluirTelefone(int fornecedorId, int telefone_id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorResponse = await _service.ExcluirTelefone(fornecedorId, telefone_id);
                    if (fornecedorResponse is null)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedorResponse);
                    return View("Detalhes", fornecedorViewModel);
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

        [HttpPost]
        [Route("[controller]/adicionar-endereco/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarEndereco(
            int fornecedorId,
            string logradouro,
            string numero,
            string complemento,
            string cep,
            string bairro,
            string cidade,
            string estado,
            string uf,
            string tipoEndereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorResponse = await _service.InserirEndereco(
                        fornecedorId, logradouro, numero, complemento, cep, bairro, cidade, estado, uf, tipoEndereco);

                    if (fornecedorResponse is null)
                        return View("Error", new ErrorViewModel().ProdutoErroAoTentarInserir());
                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedorResponse);
                    return View("Detalhes", fornecedorViewModel);
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

        [HttpGet ("[controller]/listar")]
        public async Task<IActionResult> ListarFornecedores()
        {
            try
            {
                var fornecedores = await _service.ListarFornecedores();
                if (fornecedores.Count == 0)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());// Alterar erro

                var listaViewModel = new List<FornecedorViewModel>();
                foreach (var fornecedor in fornecedores)
                    listaViewModel.Add(_mapper.CriarFornecedorViewModel(fornecedor));

                return View("Fornecedores", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpGet("[controller]/detalhes")]
        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var fornecedor = await _service.BuscarFornecedor(id);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());// Alterar erro

                var viewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                var tipoTelefone = await _service.ObterTipoTelefone();
                viewModel.TipoTelefone = new SelectList(tipoTelefone);
                viewModel.TipoEndereco = new SelectList(await _service.ObterTipoEndereco());
                foreach (var telefone in viewModel.Telefone)
                {
                    telefone.Tipo_telefone = tipoTelefone
                        .Where(x => x.Id == telefone.Tipo_telefone_id)
                        .Select(x => x.Descricao)
                        .FirstOrDefault();
                }
                return View("Detalhes", viewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [Route("[controller]/filtrar-por-setor")]
        public async Task<IActionResult> FiltrarPorSetor(string setor)
        {
            try
            {
                var fornecedores = await _service.FiltrarPorSetor(setor);
                if (fornecedores.Count == 0)
                    return View("Error", new ErrorViewModel().ErroFiltroNaoEncontrado());

                var listaViewModel = new List<FornecedorViewModel>();
                foreach (var fornecedor in fornecedores)
                    listaViewModel.Add(_mapper.CriarFornecedorViewModel(fornecedor));

                return View("Fornecedores", listaViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
        
    }
}
