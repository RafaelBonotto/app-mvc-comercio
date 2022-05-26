using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                Endereco = fornecedor.Endereco
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
                Complemento = complemento.ToUpper(),
                Cep = cep,
                Bairro = bairro.ToUpper(),
                Cidade = cidade.ToUpper(),
                Estado = estado.ToUpper(),
                UF = uf.ToUpper(),
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
                Complemento = complemento.ToUpper(),
                Cep = cep,
                Bairro = bairro.ToUpper(),
                Cidade = cidade.ToUpper(),
                Estado = estado.ToUpper(),
                UF = uf.ToUpper()
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

        public Telefone MontaInsertTelefone(string ddd, string numero, int tipoTelefone_id)
        {
            return new Telefone
            {
                Ddd = ddd,
                Numero = numero,
                Tipo_telefone_id = tipoTelefone_id,
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
    }
}
