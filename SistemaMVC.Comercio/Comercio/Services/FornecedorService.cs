using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Enums;
using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Fornecedor>> FiltrarPorSetor(string setor)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor)
        {
            if (string.IsNullOrEmpty(fornecedor.Cnpj))
            {
                var checkFornecedor = await _repositoryBase.GetByKeyAsync(fornecedor.Cnpj);
                if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 1)
                    throw new CnpjInvalidoException();
                if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 0)
                {
                    fornecedor.Id = checkFornecedor.First().Id;
                    //return await this.AtualizarFornecedor(fornecedor);
                }
            }
            var fornecedorRepository = _mapper.MontaFornecedorInsertRepositorio(fornecedor);
            var fornecedorResponse = await _repositoryBase.AddAsync(fornecedorRepository);
            return fornecedorResponse;
        }

        public async Task<bool> InserirVendedor(VendedorRequest req)
        {
            var vendedor = _mapper.MontaPessoaContato(req.Nome, req.Email);

            List<Telefone> telefones = new();
            Telefone telefone = _mapper.MontaInsertTelefoneVendedor(req.Ddd, req.Numero, TipoTelefone.COMERCIAL);
            telefones.Add(telefone);

            if (!string.IsNullOrEmpty(req.NumeroAdicional))
            {
                var telefoneAdiconal = _mapper.MontaInsertTelefoneVendedor(req.DddAdicional, req.NumeroAdicional, TipoTelefone.ADICIONAL);
                telefoneAdiconal.Tipo_telefone_id = TipoTelefone.ADICIONAL.GetHashCode();
                telefones.Add(telefoneAdiconal);
            }
            return await _repositoryFornecedor.InserirVendedor(req.Fornecedor_id, vendedor, telefones);
        }

        public async Task<bool> InserirTelefone(int fornecedor_id, string ddd, string numero, string tipoTelefone)
        {
            var telefone = _mapper.MontaInsertTelefoneVendedor(ddd, numero, TipoTelefone.COMERCIAL);
            return await _repositoryTelefone.InserirTelefoneFornecedor(fornecedor_id, telefone);
        }

        public async Task<bool> InserirEndereco(EnderecoRequest req)
        {
            var endereco = _mapper.MontarInsertEndereco(req);
            endereco.Tipo_endereco_id = await _repositoryEndereco.ObterIdTipoEndereco(req.TipoEndereco);
            return await _repositoryEndereco.InserirEnderecoFornecedor(req.Fornecedor_id, endereco);
        }

        public async Task<bool> EditarTelefone(int telefone_id, string ddd, string numero, string tipoTelefone)
        {
            var tipoTelefoneId = await _repositoryTelefone.ObterIdTipoTelefone(tipoTelefone);
            Telefone telefone = _mapper.MontaUpdateTelefone(telefone_id, ddd, numero, tipoTelefoneId);
            return await _repositoryTelefone.AtualizarTelefone(telefone);
        }

        public async Task<bool> EditarVendedor(VendedorRequest req)
        {
            return await _repositoryFornecedor.AtualizarVendedor(req);
        }

        public async Task<bool> EditarEndereco(EnderecoRequest req)
        {
            var endereco = _mapper.MontarUpdateEndereco(req);
            endereco.Tipo_endereco_id = await _repositoryEndereco.ObterIdTipoEndereco(req.TipoEndereco);
            return await _repositoryEndereco.AtualizarEndereco(endereco);
        }

        public async Task<bool> EditarNomeEmail(int fornecedor_id, string nome, string email)
        {
            var fornecedor = await _repositoryBase.GetByIdAsync(fornecedor_id);
            fornecedor.Nome_empresa = !string.IsNullOrEmpty(nome) ? nome.ToUpper() : fornecedor.Nome_empresa;
            fornecedor.Email = !string.IsNullOrEmpty(email) ? email.ToLower() : fornecedor.Email;
            fornecedor.Data_alteracao = System.DateTime.Now;
            if (await _repositoryBase.UpdateAsync(fornecedor) is null)
                return false;
            return true;
        }


        public async Task<FornecedorViewModel> RetornarForncedorViewModel(int fornecedor_id)
        {
            var fornecedorResponse = await this.BuscarFornecedor(fornecedor_id);
            var tipoTelefoneBanco = await this.ObterTipoTelefone();
            var tipoEnderecoBanco = await this.ObterTipoEndereco();
            return await _mapper.CriarFornecedorViewModel(
                fornecedorResponse, tipoTelefoneBanco, tipoEnderecoBanco);
        }

        public async Task<TelefoneFornecedorViewModel> RetornarTelefoneFornecedorViewModel(int fornecedor_id, int telefone_id)
        {
            var telefoneBanco = await _repositoryTelefone.GetById(telefone_id);
            var telefoneViewModel = _mapper.MontaTelefoneFornecedorViewModel(telefoneBanco, fornecedor_id);
            var tiposTelefone = new SelectList(await _repositoryTelefone.ListarDescricaoTipoTelefone());
            telefoneViewModel.TiposTelefoneBanco = tiposTelefone;
            return telefoneViewModel;
        }

        public async Task<EnderecoFornecedorViewModel> RetornarEnderecoFornecedorViewModel(int fornecedor_id, int endreco_id)
        {
            var enderecoBanco = await _repositoryEndereco.GetById(endreco_id);
            var enderecoViewModel = _mapper.MontaEnderecoFornecedorViewModel(enderecoBanco, fornecedor_id);
            var tiposEndereco = new SelectList(await _repositoryEndereco.ObterDescricaoTipoEndereco());
            enderecoViewModel.TiposEnderecoBanco = tiposEndereco;
            return enderecoViewModel;
        }

        public async Task<VendedorFornecedorViewModel> RetornarVendedorFornecedorViewModel(int fornecedor_id, int vendedor_id)
        {
            var vendedorBanco = await _repositoryFornecedor.GetVendedor(vendedor_id);
            var telefonesVendedor = await _repositoryFornecedor.GetTelefoneVendedor(vendedor_id);
            var VendedorViewModel = _mapper.MontaVendedorFornecedorViewModel(vendedorBanco, telefonesVendedor, fornecedor_id);
            return VendedorViewModel;
        }

        public Task<bool> ExcluirVendedor(int fornecedor_id, int vendedor_id)
            => _repositoryFornecedor.ExcluirVendedor(fornecedor_id, vendedor_id);

        public async Task<bool> ExcluirEndereco(int fornecedor_id, int endereco_id)
           => await _repositoryEndereco.ExcluirEnderecoFornecedor(fornecedor_id, endereco_id);

        public async Task<bool> ExcluirTelefone(int fornecedor_id, int telefone_id)
           => await _repositoryTelefone.ExcluirTelefoneFornecedor(fornecedor_id, telefone_id);

        public async Task<List<Fornecedor>> ListarFornecedores()
            => await _repositoryBase.GetAllAsync();

        public async Task<Fornecedor> BuscarFornecedor(int id)
            => await _repositoryBase.GetByIdAsync(id);

        public async Task<List<TipoEnderecoResponse>> ObterTipoEndereco()
            => await _repositoryEndereco.ObterDescricaoTipoEndereco();

        public async Task<List<TipoTelefoneResponse>> ObterTipoTelefone()
            => await _repositoryTelefone.ListarDescricaoTipoTelefone();
    }
}
