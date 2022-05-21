using Comercio.Entities;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;

namespace Comercio.Mapper
{
    public class FornecedorAdapter : IFornecedorAdapter
    {
        public FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor)
        {
            return new FornecedorViewModel()
            {
                Id = fornecedor.Id,
                Cnpj = fornecedor.Cnpj,
                Nome_empresa = fornecedor.Nome_empresa.ToUpper(),
                Telefone = fornecedor.Telefone,
                Endereco = fornecedor.Endereco
            };
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
    }
}
