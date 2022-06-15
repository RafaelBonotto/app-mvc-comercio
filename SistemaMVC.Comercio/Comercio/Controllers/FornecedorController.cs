using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
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

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
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

        [Route("[controller]/atualizar-telefone/")]
        public async Task<IActionResult> MontaViewModelAtualizarTelefone(int fornecedor_id, int telefone_id)
        {
            try
            {
                var telefoneViewModel = await _service.RetornarTelefoneFornecedorViewModel(fornecedor_id, telefone_id);
                // VALIDAÇÃO NO RETORNO ?
                return View("EditarTelefone", telefoneViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("[controller]/editar-telefone/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTelefone(int fornecedor_id, int telefone_id, string ddd, string numero, string tipo_telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var update = await _service.EditarTelefone(telefone_id, ddd, numero, tipo_telefone);
                    if (!update)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // Erro ao tentar atualizar o telefone...

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
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

                var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
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
        public async Task<IActionResult> AdicionarEndereco(EnderecoRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var insert = await _service.InserirEndereco(request);
                    if (!insert)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // MSG ERRO

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(request.Fornecedor_id);
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

        [Route("[controller]/atualizar-endereco/")]
        public async Task<IActionResult> MontaViewModelAtualizarEndereco(int fornecedor_id, int endereco_id)
        {
            try
            {
                var EnderecoViewModel = await _service.RetornarEnderecoFornecedorViewModel(fornecedor_id, endereco_id);
                // VALIDAÇÃO NO RETORNO ?
                return View("EditarEndereco", EnderecoViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("[controller]/editar-endereco/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEndereco(int fornecedor_id, EnderecoRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var update = await _service.EditarEndereco(request);

                    if (!update)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // MSG ERRO 

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
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

                var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
                return View("Detalhes", fornecedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("[controller]/adicionar-vendedor/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarVendedor(VendedorRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var insert = await _service.InserirVendedor(request);
                    if (!insert)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // ERRO AO TENTAR ADICIONAR

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(request.Fornecedor_id);
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

        [Route("[controller]/atualizar-vendedor/")]
        public async Task<IActionResult> MontaViewModelAtualizarVendedor(int fornecedor_id, int vendedor_id)
        {
            try
            {
                var vendedorViewModel = await _service.RetornarVendedorFornecedorViewModel(fornecedor_id, vendedor_id);
                // VALIDAÇÃO NO RETORNO ?
                return View("EditarVendedor", vendedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpPost]
        [Route("[controller]/editar-vendedor/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarVendedor(VendedorRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var update = await _service.EditarVendedor(req);
                    if (!update)
                        return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // Erro ao tentar atualizar 

                    var fornecedorViewModel = await _service.RetornarForncedorViewModel(req.Fornecedor_id);
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

        [HttpGet("[controller]/excluir-vendedor")]
        public async Task<IActionResult> ExcluirVendedor(int fornecedor_id, int vendedor_id)
        {
            try
            {
                var delete = await _service.ExcluirVendedor(fornecedor_id, vendedor_id);
                if (!delete)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina()); // ERRO AO TENTAR EXCLUIR

                var fornecedorViewModel = await _service.RetornarForncedorViewModel(fornecedor_id);
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

                var fornecedorViewModel = await _service.RetornarForncedorViewModel(id);
                return View("Detalhes", fornecedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }

        [HttpGet("[controller]/exibirFornecedor")]
        public async Task<IActionResult> ExibirFornecedor(int id) 
        {
            try
            {
                var fornecedor = await _service.BuscarFornecedor(id);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());// Alterar erro

                var fornecedorViewModel = await _service.RetornarForncedorViewModel(id);
                return View("ExibirFornecedor", fornecedorViewModel);
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
