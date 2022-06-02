using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        Telefone MontaUpdateTelefone(int telefone_id, string ddd, string numero, int tipoTelefone_id);
        Telefone MontaInsertTelefone(string ddd, string numero, int tipoTelefone_id);
        Endereco MontarInsertEndereco(
            string logradouro, string numero, string complemento, string cep, 
            string bairro, string cidade, string estado, string uf);
        Endereco MontarUpdateEndereco(int id,
            string logradouro, string numero, string complemento, string cep, 
            string bairro, string cidade, string estado, string uf);
        EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId);
        PessoaContatoFornecedor MontaInsertVendedorFornecedor(int fornecedorId, int vendedorId);
        PessoaContatoTelefone MontaInsertVendedorTelefone(int vendedorId, int telefoneId);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
        Task<FornecedorViewModel> CriarFornecedorViewModel(
            Fornecedor fornecedor,
            List<TipoTelefoneResponse> tipoTelRepositorio,
            List<TipoEnderecoResponse> tipoEndRepositorio);
        TelefoneFornecedorViewModel MontaTelefoneFornecedorViewModel(Telefone telefone, int fornecedor_id);
        EnderecoFornecedorViewModel MontaEnderecoFornecedorViewModel(Endereco endereco, int fornecedor_id);
    }
}
