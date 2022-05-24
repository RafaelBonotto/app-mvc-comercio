using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _service;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorController(
            IFornecedorService fornecedorService,
            IFornecedorAdapter adaper)
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
        public async Task<IActionResult> AdicionarTelefone(int fornecedor_id, string ddd, string numero, string tipoTelefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var insert = await _service.InserirTelefone(fornecedor_id, ddd, numero, tipoTelefone);
                    if (!insert)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // ERRO AO TENTAR ADICIONAR

                    var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedor_id);
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
        [Route("[controller]/editar-telefone/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTelefone(int fornecedor_id, int telefone_id, string ddd, string numero, string tipoTelefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var update = await _service.EditarTelefone(telefone_id, ddd, numero, tipoTelefone);
                    if (!update)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // Erro ao tentar atualizar o telefone...

                    var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedor_id);
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

        [HttpGet("[controller]/excluir-telefone")]
        public async Task<IActionResult> ExcluirTelefone(int fornecedor_id, int telefone_id)
        {
            try
            {
                var delete = await _service.ExcluirTelefone(fornecedor_id, telefone_id);
                if (!delete)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // ERRO AO TENTAR EXCLUIR

                var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedor_id);
                return View("Detalhes", fornecedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
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
                    var insert = await _service.InserirEndereco(
                        fornecedorId, logradouro, numero, complemento, cep, bairro, cidade, estado, uf, tipoEndereco);

                    if (!insert)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // MSG ERRO

                    var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedorId);
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
        [Route("[controller]/editar-endereco/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEndereco(
            int fornecedor_id,
            int endereco_id,
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
                    var update = await _service.AtualizarEndereco(
                        endereco_id, logradouro, numero, complemento, cep, bairro, cidade, estado, uf, tipoEndereco);

                    if (!update)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // MSG ERRO 

                    var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedor_id);
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

        [HttpGet("[controller]/excluir-endereco")]
        public async Task<IActionResult> ExcluirEndereco(int fornecedor_id, int endereco_id)
        {
            try
            {
                var delete = await _service.ExcluirEndereco(fornecedor_id, endereco_id);
                if (!delete)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // MSG ERRO AO TENTAR EXCLUIR

                var fornecedorViewModel = _service.RetornarForncedorViewModel(fornecedor_id);
                return View("Detalhes", fornecedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpGet("[controller]/listar")]
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
                var tipoTelefone = await _service.ObterTipoTelefone();
                var tipoEndereco = await _service.ObterTipoEndereco();
                var viewModel = _mapper.CriarFornecedorViewModel(fornecedor, tipoTelefone, tipoEndereco);
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
