using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

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
        EnderecoFornecedor MontaEnderecoFornecedor(int fornecedorId, int enderecoId);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
        FornecedorViewModel CriarFornecedorViewModel(
            Fornecedor fornecedor,
            List<TipoTelefoneResponse> tipoTelRepositorio,
            List<TipoEnderecoResponse> tipoEndRepositorio);
    }
}
