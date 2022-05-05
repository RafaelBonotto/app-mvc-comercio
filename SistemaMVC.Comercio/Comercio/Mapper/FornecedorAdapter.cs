using Comercio.Entities;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using System;

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
                Nome_empresa = fornecedor.Nome_empresa.ToUpper()
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

        public Endereco RetornarEnderecoToUpper(Endereco endereco)
        {
            return new Endereco()
            {
                Logradouro = endereco.Logradouro.ToUpper(),
                NumeroLogradouro = endereco.NumeroLogradouro.ToUpper(),
                Complemento =  endereco.Complemento.ToUpper(),
                Cep = endereco.Cep.ToUpper(),
                Bairro = endereco.Bairro.ToUpper(),
                Cidade = endereco.Bairro.ToUpper(),
                Estado = endereco.Bairro.ToUpper(),
                Tipo = endereco.Tipo,
            };
        }

        public TelefoneFornecedor MontaFornecedorTelefone(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }
    }
}
