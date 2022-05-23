﻿using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.EnderecoInterfaces;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Interfaces.TelefoneInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IRepositoryBase<Fornecedor> Fornecedor;
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
            Fornecedor = repository;
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
                var checkFornecedor = await Fornecedor.GetByKeyAsync(fornecedor.Cnpj);
                if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 1)
                    throw new CnpjInvalidoException();
                if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 0)
                {
                    fornecedor.Id = checkFornecedor.First().Id;
                    //return await this.AtualizarFornecedor(fornecedor);
                }
            }
            var fornecedorRepository = _mapper.MontaFornecedorInsertRepositorio(fornecedor);
            var fornecedorResponse = await Fornecedor.AddAsync(fornecedorRepository);
            return fornecedorResponse;
        }

        public async Task<bool> InserirTelefone(int fornecedor_id, string ddd, string numero, string tipoTelefone)
        {
            var tipoTelefoneId = await _repositoryTelefone.ObterIdTipoTelefone(tipoTelefone);
            var telefone = _mapper.MontaInsertTelefone(ddd, numero, tipoTelefoneId);
            return await _repositoryTelefone.InserirTelefoneFornecedor(fornecedor_id, telefone);
        }

        public async Task<bool> EditarTelefone(int telefone_id, string ddd, string numero, string tipoTelefone)
        {
            var tipoTelefoneId = await _repositoryTelefone.ObterIdTipoTelefone(tipoTelefone);
            Telefone telefone = _mapper.MontaUpdateTelefone(telefone_id, ddd, numero, tipoTelefoneId);
            return await _repositoryTelefone.AtualizarTelefone(telefone);
        }

        public async Task<bool> InserirEndereco(
            int fornecedor_id,
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
            var endereco = _mapper.MontarInsertEndereco(
                    logradouro: logradouro,
                    numero: numero,
                    complemento: complemento,
                    cep: cep,
                    bairro: bairro,
                    cidade: cidade,
                    estado: estado,
                    uf: uf);
            endereco.Tipo_endereco_id = await _repositoryEndereco.ObterIdTipoEndereco(tipoEndereco);
            return await _repositoryEndereco.InserirEnderecoFornecedor(fornecedor_id, endereco);
        }

        public async Task<bool> EditarEndereco(
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
            var endereco = _mapper.MontarInsertEndereco(
                    logradouro: logradouro,
                    numero: numero,
                    complemento: complemento,
                    cep: cep,
                    bairro: bairro,
                    cidade: cidade,
                    estado: estado,
                    uf: uf);
            endereco.Tipo_endereco_id = await _repositoryEndereco.ObterIdTipoEndereco(tipoEndereco);
            return await _repositoryEndereco.EditarEnderecoFornecedor(endereco);
        }

        public async Task<bool> ExcluirEndereco(int fornecedor_id, int endereco_id)
           => await _repositoryEndereco.ExcluirEnderecoFornecedor(fornecedor_id, endereco_id);

        public async Task<List<Fornecedor>> ListarFornecedores()
            => await Fornecedor.GetAllAsync();

        public async Task<Fornecedor> BuscarFornecedor(int id)
            => await Fornecedor.GetByIdAsync(id);
            
        public async Task<List<TipoEnderecoResponse>> ObterTipoEndereco()
            => await _repositoryEndereco.ObterDescricaoTipoEndereco();

        public async Task<List<TipoTelefoneResponse>> ObterTipoTelefone()
            => await _repositoryTelefone.ListarDescricaoTipoTelefone();

        public async Task<bool> ExcluirTelefone(int fornecedor_id, int telefone_id)
            => await _repositoryTelefone.ExcluirTelefoneFornecedor(fornecedor_id, telefone_id);
    }
}
