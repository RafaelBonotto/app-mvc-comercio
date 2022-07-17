using Comercio.Entities;
using Comercio.Enums;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IRepositoryBase<Fornecedor> _repositoryBase;
        private readonly IFornecedorRepository _repositoryFornecedor;
        private readonly ITelefoneRepository _repositoryTelefone;
        private readonly IEnderecoRepository _repositoryEndereco;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorService(
            IRepositoryBase<Fornecedor> repository, 
            IFornecedorRepository fornecedorRepository, 
            IFornecedorAdapter mapper,
            ITelefoneRepository repositoryTelefone,
            IEnderecoRepository repositoryEndereco)
        {
            _repositoryBase = repository;
            _repositoryFornecedor = fornecedorRepository;
            _repositoryTelefone = repositoryTelefone;
            _repositoryEndereco = repositoryEndereco;
            _mapper = mapper;
        }

        public async Task<List<Fornecedor>> FiltrarPorSetor(string setor)
            => await _repositoryFornecedor.FiltarPorSetor(setor);

        public async Task<List<Fornecedor>> FiltrarPorNome(string nome)
            => await _repositoryFornecedor.FiltarPorNome(nome);

        public async Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor)
        {
            var fornecedorRepository = _mapper.MontaFornecedorInsertRepositorio(fornecedor);
            var fornecedorResponse = await _repositoryBase.AddAsync(fornecedorRepository);
            return fornecedorResponse;
        }

        public async Task<bool> ExcluirFornecedor(int id)
            => await _repositoryFornecedor.ExcluirFornecedor(id);

        public async Task<Fornecedor> InserirVendedor(VendedorRequest req)
        {
            var vendedor = _mapper.MontaPessoaContato(req.Nome, req.Email);

            List<Telefone> telefones = new();
            Telefone telefone = _mapper.MontaInsertTelefone(req.Ddd, req.Numero, TipoTelefone.COMERCIAL);
            telefones.Add(telefone);

            if (!string.IsNullOrEmpty(req.NumeroAdicional))
            {
                var telefoneAdiconal = _mapper.MontaInsertTelefone(req.DddAdicional, req.NumeroAdicional, TipoTelefone.ADICIONAL);
                telefoneAdiconal.Tipo_telefone_id = TipoTelefone.ADICIONAL.GetHashCode();
                telefones.Add(telefoneAdiconal);
            }
            return await _repositoryFornecedor.InserirVendedor(req.Fornecedor_id, vendedor, telefones);
        }

        public async Task<Fornecedor> InserirTelefone(TelefoneRequest req)
        {
            var telefone = _mapper.MontaInsertTelefone(req.Ddd, req.Numero, TipoTelefone.COMERCIAL);
            return await _repositoryFornecedor.InserirTelefone(req.Fornecedor_id, telefone);
        }

        public async Task<Fornecedor> InserirEndereco(EnderecoRequest req)
            => await _repositoryFornecedor.InserirEndereco(req);

        public async Task<Fornecedor> EditarTelefone(TelefoneRequest req)
            => await _repositoryFornecedor.EditarTelefone(req);

        public async Task<Fornecedor> EditarVendedor(VendedorRequest req)
            => await _repositoryFornecedor.AtualizarVendedor(req);

        public async Task<Fornecedor> EditarEndereco(EnderecoRequest req)
            => await _repositoryFornecedor.EditarEndereco(req);

        public async Task<Fornecedor> EditarNomeEmail(int fornecedor_id, string nome, string email)
        {
            Fornecedor fornecedor = new();
            fornecedor.Id = fornecedor_id;
            fornecedor.Nome_empresa = !string.IsNullOrEmpty(nome) ? nome.ToUpper() : string.Empty;
            fornecedor.Email = !string.IsNullOrEmpty(email) ? email.ToLower() : string.Empty;
            return await _repositoryBase.UpdateAsync(fornecedor);
        }

        public async Task<TelefoneFornecedorViewModel> RetornarTelefoneFornecedorViewModel(int fornecedor_id, int telefone_id)
        {
            var telefoneBanco = await _repositoryTelefone.GetById(telefone_id);
            if (telefoneBanco is null)
                return null;
            var telefoneViewModel = _mapper.MontaTelefoneFornecedorViewModel(telefoneBanco, fornecedor_id);
            return telefoneViewModel;
        }

        public async Task<EnderecoFornecedorViewModel> RetornarEnderecoFornecedorViewModel(int fornecedor_id, int endreco_id)
        {
            var enderecoBanco = await _repositoryEndereco.GetById(endreco_id);
            if (enderecoBanco is null)
                return null;
            var enderecoViewModel = _mapper.MontaEnderecoFornecedorViewModel(enderecoBanco, fornecedor_id);
            return enderecoViewModel;
        }

        public async Task<VendedorFornecedorViewModel> RetornarVendedorFornecedorViewModel(int fornecedor_id, int vendedor_id)
        {
            var vendedorBanco = await _repositoryFornecedor.GetVendedor(vendedor_id);
            if (vendedorBanco is null)
                return null;
            var VendedorViewModel = _mapper.MontaVendedorFornecedorViewModel(vendedorBanco, fornecedor_id);
            return VendedorViewModel;
        }

        public Task<Fornecedor> ExcluirVendedor(int fornecedor_id, int vendedor_id)
            => _repositoryFornecedor.ExcluirVendedor(fornecedor_id, vendedor_id);

        public async Task<Fornecedor> ExcluirEndereco(int fornecedor_id, int endereco_id)
           => await _repositoryFornecedor.ExcluirEndereco(fornecedor_id, endereco_id);

        public async Task<Fornecedor> ExcluirTelefone(int fornecedor_id, int telefone_id)
           => await _repositoryFornecedor.ExcluirTelefone(fornecedor_id, telefone_id);

        public async Task<List<Fornecedor>> ListarFornecedores()
            => await _repositoryBase.GetAllAsync();

        public async Task<List<Produto>> ListarProdutos(int fornecedor_id)
            => await _repositoryFornecedor.ListarProdutos(fornecedor_id);

        public async Task<Fornecedor> BuscarFornecedor(int id)
            => await _repositoryBase.GetByIdAsync(id);

        public async Task<List<Setor>> ListarSetores() 
            => await _repositoryFornecedor.ObterSetores();
    }
}
