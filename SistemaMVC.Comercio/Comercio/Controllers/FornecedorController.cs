using Comercio.Exceptions.Fornecedor;
using Comercio.Extensions;
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
        public async Task<IActionResult> Adicionar(FornecedorViewModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorResponse = await _service.InserirFornecedor(fornecedor);
                    if (fornecedorResponse is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarInserir()); 

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
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpPost]
        [Route("[controller]/editarNomeEmail/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarNomeEmail(EditarNomeEmailRequest req)
            
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedor = await _service.EditarNomeEmail(req.Fornecedor_id, req.Nome, req.Email);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarAtualizarNomeEmail()); 

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpPost]
        [Route("[controller]/adicionar-telefone/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarTelefone(TelefoneRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedor = await _service.InserirTelefone(req);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarInserirTelefone());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [Route("[controller]/atualizar-telefone/")]
        public async Task<IActionResult> MontaViewModelAtualizarTelefone(int fornecedor_id, int telefone_id)
        {
            try
            {
                var telefoneViewModel = await _service.RetornarTelefoneFornecedorViewModel(fornecedor_id, telefone_id);
                if (telefoneViewModel is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarCarregarTelefone());

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
        public async Task<IActionResult> EditarTelefone(TelefoneRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedor = await _service.EditarTelefone(req);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarEditarTelefone());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception ex)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpGet("[controller]/excluir-telefone")]
        public async Task<IActionResult> ExcluirTelefone(int fornecedor_id, int telefone_id)
        {
            try
            {
                var fornecedor = await _service.ExcluirTelefone(fornecedor_id, telefone_id);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarExcluirTelefone());

                var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
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
                    var fornecedor = await _service.InserirEndereco(request);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarInserirEndereco());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [Route("[controller]/atualizar-endereco/")]
        public async Task<IActionResult> MontaViewModelAtualizarEndereco(int fornecedor_id, int endereco_id)
        {
            try
            {
                var EnderecoViewModel = await _service.RetornarEnderecoFornecedorViewModel(fornecedor_id, endereco_id);
                if(EnderecoViewModel is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarCarregarEndereco());

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
                    var fornecedor = await _service.EditarEndereco(request);

                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarEditarEndereco());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception ex)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpGet("[controller]/excluir-endereco")]
        public async Task<IActionResult> ExcluirEndereco(int fornecedor_id, int endereco_id)
        {
            try
            {
                var fornecedor = await _service.ExcluirEndereco(fornecedor_id, endereco_id);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarExcluirEndereco());

                var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
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
                    var fornecedor = await _service.InserirVendedor(request);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarInserirVendedor());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [Route("[controller]/atualizar-vendedor/")]
        public async Task<IActionResult> MontaViewModelAtualizarVendedor(int fornecedor_id, int vendedor_id)
        {
            try
            {
                var vendedorViewModel = await _service.RetornarVendedorFornecedorViewModel(fornecedor_id, vendedor_id);
                if(vendedorViewModel is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarCarregarVendedor());

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
                    var fornecedor = await _service.EditarVendedor(req);
                    if (fornecedor is null)
                        return View("Error", new ErrorViewModel().FornecedorErroAoTentarEditarVendedor());

                    var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                    return View("Detalhes", fornecedorViewModel);
                }
                catch (System.Exception)
                {
                    return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
                }
            }
            else
            {
                return View("Error", new ErrorViewModel().ErroDeValidacao(ModelState.GetErros()));
            }
        }

        [HttpGet("[controller]/excluir-vendedor")]
        public async Task<IActionResult> ExcluirVendedor(int fornecedor_id, int vendedor_id)
        {
            try
            {
                var fornecedor = await _service.ExcluirVendedor(fornecedor_id, vendedor_id);
                if (fornecedor is null)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarExcluirVendedor());

                var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
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
                    return View("Error", new ErrorViewModel().FornecedorErroAoBuscarFornecedor());

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
                    return View("Error", new ErrorViewModel().FornecedorErroAoBuscarFornecedor());

                var fornecedorViewModel =_mapper.CriarFornecedorViewModel(fornecedor);
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
                    return View("Error", new ErrorViewModel().FornecedorErroAoBuscarFornecedor());

                var fornecedorViewModel = _mapper.CriarFornecedorViewModel(fornecedor);
                return View("ExibirFornecedor", fornecedorViewModel);
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
        [HttpGet("[controller]/excluir")]
        public async Task<IActionResult> ExcluirFornecedor(int id)
        {
            try
            {
                var delete = await _service.ExcluirFornecedor(id);
                if (!delete)
                    return View("Error", new ErrorViewModel().FornecedorErroAoTentarExcluir());

                return View("Index");
            }
            catch (System.Exception)
            {
                return View("Error", new ErrorViewModel().ErroAoTentarCarregarPagina());
            }
        }
    }
}
