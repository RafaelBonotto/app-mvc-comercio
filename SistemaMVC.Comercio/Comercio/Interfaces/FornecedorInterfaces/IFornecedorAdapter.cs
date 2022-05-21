using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        Endereco MontarInsertEndereco(
            string logradouro, string numero, string complemento, string cep, 
            string bairro, string cidade, string estado, string uf);
        EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId);
        //TelefoneFornecedor MontaTelefoneFornecedor(int fornecedorId, int telefoneId);
        //List<FornecedorVendedor> MontaVendedorFornecedor(FornecedorViewModel fornecedor);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
    }
}
