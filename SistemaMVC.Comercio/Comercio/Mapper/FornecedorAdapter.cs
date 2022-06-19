using Comercio.Entities;
using Comercio.Enums;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comercio.Mapper
{
    public class FornecedorAdapter : IFornecedorAdapter
    {
        public FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor)
        {
            var ret = new FornecedorViewModel
            {
                Id = fornecedor.Id,
                Cnpj = fornecedor.Cnpj,
                Email = fornecedor.Email,
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Telefone = fornecedor.Telefone,
                Endereco = fornecedor.Endereco,
                Vendedor = fornecedor.Vendedor
            };
            ret.TipoTelefone = new SelectList(fornecedor.DescricaoTipoTelefone);
            ret.TipoEndereco = new SelectList(fornecedor.DescricaoTipoEndereco);
            return ret;
        }

        public EnderecoFornecedor MontaFornecedorEndereco(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }

        public Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor)
        {
            return new Fornecedor()
            {
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Cnpj = fornecedor.Cnpj,
                Email = fornecedor.Email.ToLower(),
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public Endereco MontarInsertEndereco(EnderecoRequest req)
        {
            return new Endereco()
            {
                Logradouro = req.Logradouro.ToUpper(),
                Numero = req.Numero,
                Complemento = string.IsNullOrEmpty(req.Complemento) ? string.Empty : req.Complemento.ToUpper(),
                Cep = string.IsNullOrEmpty(req.Complemento) ? string.Empty : req.Cep,
                Bairro = string.IsNullOrEmpty(req.Bairro) ? string.Empty : req.Bairro.ToUpper(),
                Cidade = string.IsNullOrEmpty(req.Cidade) ? string.Empty : req.Cidade.ToUpper(),
                Estado = string.IsNullOrEmpty(req.Estado) ? string.Empty : req.Estado.ToUpper(),
                UF = string.IsNullOrEmpty(req.Uf) ? string.Empty : req.Uf.ToUpper(),
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public Endereco MontarUpdateEndereco(EnderecoRequest req)
        {
            return new Endereco()
            {
                Id = req.Endereco_id,
                Logradouro = req.Logradouro.ToUpper(),
                Numero = req.Numero,
                Complemento = string.IsNullOrEmpty(req.Complemento) ? string.Empty : req.Complemento.ToUpper(),
                Cep = string.IsNullOrEmpty(req.Cep) ? string.Empty : req.Cep,
                Bairro = string.IsNullOrEmpty(req.Bairro) ? string.Empty : req.Bairro.ToUpper(),
                Cidade = string.IsNullOrEmpty(req.Cidade) ? string.Empty : req.Cidade.ToUpper(),
                Estado = string.IsNullOrEmpty(req.Estado) ? string.Empty : req.Estado.ToUpper(),
                UF = string.IsNullOrEmpty(req.Uf) ? string.Empty : req.Uf.ToUpper()
            };
        }

        public EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId)
        {
            return new EnderecoFornecedor()
            {
                Fornecedor_id = fornecedorId,
                Endereco_id = enderecoId,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public Telefone MontaInsertTelefone(string ddd, string numero, TipoTelefone tipoTelefone)
        {
            return new Telefone
            {
                Ddd = ddd,
                Numero = numero,
                Tipo_telefone_id = tipoTelefone.GetHashCode(),
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public Telefone MontaUpdateTelefone(int telefone_id, string ddd, string numero, int tipoTelefone_id)
        {
            return new Telefone
            {
                Id = telefone_id,
                Ddd = ddd,
                Numero = numero,
                Tipo_telefone_id = tipoTelefone_id
            };
        }

        public TelefoneFornecedorViewModel MontaTelefoneFornecedorViewModel(Telefone telefone, int fornecedor_id)
        {
            return new TelefoneFornecedorViewModel
            {
                Telefone_id = telefone.Id,
                Fornecedor_id = fornecedor_id,
                Ddd = telefone.Ddd,
                Numero = telefone.Numero,
                Tipo_telefone_id = telefone.Tipo_telefone_id
            };
        }

        public EnderecoFornecedorViewModel MontaEnderecoFornecedorViewModel(Endereco endereco, int fornecedor_id)
        {
            return new EnderecoFornecedorViewModel
            {
                Endereco_id = endereco.Id,
                Fornecedor_id = fornecedor_id,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Cep = endereco.Cep,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                UF = endereco.UF,
                Tipo_endereco_id = endereco.Tipo_endereco_id
            };
        }

        public VendedorFornecedorViewModel MontaVendedorFornecedorViewModel(PessoaContato vendedor, List<Telefone> telefone, int fornecedor_id)
        {
            return new VendedorFornecedorViewModel
            {
                Fornecedor_id = fornecedor_id,
                Vendedor_id = vendedor.Id,
                Nome = vendedor.Nome,
                Email = vendedor.Email,
                Ddd = telefone.First().Ddd,
                Numero = telefone.First().Numero,
                DddAdicional = telefone.Count > 1 ? telefone.Last().Ddd : string.Empty,
                NumeroAdicional = telefone.Count > 1 ? telefone.Last().Numero : string.Empty
            };
        }

        public PessoaContatoFornecedor MontaInsertVendedorFornecedor(int fornecedorId, int vendedorId)
        {
            return new PessoaContatoFornecedor
            {
                Fornecedor_id = fornecedorId,
                Pessoa_Contato_id = vendedorId,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public PessoaContatoTelefone MontaInsertVendedorTelefone(int vendedorId, int telefoneId)
        {
            return new PessoaContatoTelefone
            {
                Pessoa_contato_id = vendedorId,
                Telefone_id = telefoneId,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public PessoaContato MontaPessoaContato(string nome, string email)
        {
            return new PessoaContato
            {
                Nome = nome,
                Email = email,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }
    }
}
