using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Enums;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Mapper
{
    public class FornecedorAdapter : IFornecedorAdapter
    {
        public FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor)
        {
            return new FornecedorViewModel
            {
                Id = fornecedor.Id,
                Cnpj = fornecedor.Cnpj,
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Telefone = fornecedor.Telefone,
                Endereco = fornecedor.Endereco
            };
        }

        public async Task<FornecedorViewModel> CriarFornecedorViewModel(
            Fornecedor fornecedor, 
            List<TipoTelefoneResponse> tipoTelRepositorio, 
            List<TipoEnderecoResponse> tipoEndRepositorio)
        {
            foreach (var telefone in fornecedor.Telefone)
            {
                telefone.Tipo_telefone = tipoTelRepositorio
                    .Where(x => x.Id == telefone.Tipo_telefone_id)
                    .Select(x => x.Descricao)
                    .FirstOrDefault();
            }
            foreach (var endereco in fornecedor.Endereco)
            {
                endereco.Tipo_endereco = tipoEndRepositorio
                    .Where(x => x.Id == endereco.Tipo_endereco_id)
                    .Select(x => x.Descricao)
                    .FirstOrDefault();
            }
            var ret = new FornecedorViewModel()
            {
                Id = fornecedor.Id,
                Cnpj = fornecedor.Cnpj,
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Telefone = fornecedor.Telefone,
                Endereco = fornecedor.Endereco,
                Vendedor = fornecedor.Vendedor
            };
            ret.TipoTelefone = new SelectList(tipoTelRepositorio);
            ret.TipoEndereco = new SelectList(tipoEndRepositorio);
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
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now,
                Endereco = fornecedor.Endereco,
                Telefone = fornecedor.Telefone,
                Vendedor = fornecedor.Vendedor
            };
        }

        public Endereco MontarInsertEndereco(
            string logradouro,
            string numero,
            string complemento,
            string cep,
            string bairro,
            string cidade,
            string estado,
            string uf)
        {
            return new Endereco()
            {
                Logradouro = logradouro.ToUpper(),
                Numero = numero,
                Complemento = string.IsNullOrEmpty(complemento) ? string.Empty : complemento.ToUpper(),
                Cep = string.IsNullOrEmpty(complemento) ? string.Empty : cep,
                Bairro = string.IsNullOrEmpty(bairro) ? string.Empty : bairro.ToUpper(),
                Cidade = string.IsNullOrEmpty(cidade) ? string.Empty : cidade.ToUpper(),
                Estado = string.IsNullOrEmpty(estado) ? string.Empty : estado.ToUpper(),
                UF = string.IsNullOrEmpty(uf) ? string.Empty : uf.ToUpper(),
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }

        public Endereco MontarUpdateEndereco(
            int id,
            string logradouro,
            string numero,
            string complemento,
            string cep,
            string bairro,
            string cidade,
            string estado,
            string uf)
        {
            return new Endereco()
            {
                Id = id,
                Logradouro = logradouro.ToUpper(),
                Numero = numero,
                Complemento = string.IsNullOrEmpty(complemento) ? string.Empty : complemento.ToUpper(),
                Cep = string.IsNullOrEmpty(complemento) ? string.Empty : cep,
                Bairro = string.IsNullOrEmpty(bairro) ? string.Empty : bairro.ToUpper(),
                Cidade = string.IsNullOrEmpty(cidade) ? string.Empty : cidade.ToUpper(),
                Estado = string.IsNullOrEmpty(estado) ? string.Empty : estado.ToUpper(),
                UF = string.IsNullOrEmpty(uf) ? string.Empty : uf.ToUpper()
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

        public Telefone MontaInsertTelefoneVendedor(string ddd, string numero)
        {
            return new Telefone
            {
                Ddd = ddd,
                Numero = numero,
                Tipo_telefone_id = TipoTelefone.COMERCIAL.GetHashCode(),
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
                DddAdicional = telefone.Last().Ddd,
                NumeroAdicional = telefone.Last().Numero,
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
