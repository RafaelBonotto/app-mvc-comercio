using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Enums;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        Telefone MontaUpdateTelefone(int telefone_id, string ddd, string numero, int tipoTelefone_id);
        Telefone MontaInsertTelefoneVendedor(string ddd, string numero, TipoTelefone tipoTelefone);
        Endereco MontarInsertEndereco(EnderecoRequest req);
        Endereco MontarUpdateEndereco(EnderecoRequest req);
        EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId);
        PessoaContato MontaPessoaContato(string nome, string email);
        PessoaContatoFornecedor MontaInsertVendedorFornecedor(int fornecedorId, int vendedorId);
        PessoaContatoTelefone MontaInsertVendedorTelefone(int vendedorId, int telefoneId);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
        Task<FornecedorViewModel> CriarFornecedorViewModel(
            Fornecedor fornecedor,
            List<TipoTelefoneResponse> tipoTelRepositorio,
            List<TipoEnderecoResponse> tipoEndRepositorio);
        TelefoneFornecedorViewModel MontaTelefoneFornecedorViewModel(Telefone telefone, int fornecedor_id);
        EnderecoFornecedorViewModel MontaEnderecoFornecedorViewModel(Endereco endereco, int fornecedor_id);
        VendedorFornecedorViewModel MontaVendedorFornecedorViewModel(PessoaContato vendedor, List<Telefone> telefone, int fornecedor_id);
    }
}
