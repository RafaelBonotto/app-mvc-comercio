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
            throw new System.NotImplementedException();
        }

        public FornecedorEndereco MontaFornecedorEndereco(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }

        public Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor)
        {
            return new Fornecedor()
            {
                Nome_empresa = fornecedor.Nome_empresa,
                Cnpj = fornecedor.Cnpj,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now,
                Endereco = fornecedor.Endereco,
                Telefone = fornecedor.Telefone,
                Vendedor = fornecedor.Vendedor
            };
        }

        public FornecedorTelefone MontaFornecedorTelefone(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }
    }
}
