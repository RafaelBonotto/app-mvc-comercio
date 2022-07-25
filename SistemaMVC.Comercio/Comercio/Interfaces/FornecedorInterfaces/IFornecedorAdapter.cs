using Comercio.Entities;
using Comercio.Enums;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using System.Collections.Generic;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        Telefone MontaUpdateTelefone(int telefone_id, string ddd, string numero, int tipoTelefone_id);
        Telefone MontaInsertTelefone(string ddd, string numero, TipoTelefone tipoTelefone);
        Endereco MontarInsertEndereco(EnderecoRequest req);
        Endereco MontarUpdateEndereco(EnderecoRequest req);
        EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId);
        PessoaContato MontaPessoaContato(string nome, string email);
        PessoaContatoFornecedor MontaInsertVendedorFornecedor(int fornecedorId, int vendedorId);
        PessoaContatoTelefone MontaInsertVendedorTelefone(int vendedorId, int telefoneId);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
        TelefoneFornecedorViewModel MontaTelefoneFornecedorViewModel(Telefone telefone, int fornecedor_id);
        EnderecoFornecedorViewModel MontaEnderecoFornecedorViewModel(Endereco endereco, int fornecedor_id);
        VendedorFornecedorViewModel MontaVendedorFornecedorViewModel(PessoaContato vendedor, int fornecedor_id);
        List<ProdutoViewModel> MontaForncedorProdutoViewModel(List<Produto> produtos);
    }
}
