using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorService 
    {
        Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor);
        Task<Fornecedor> InserirTelefone(int fornecedor_id, string ddd, string numero, string tipoTelefone);
        Task<Fornecedor> InserirEndereco(int fornecedor_id, string logradouro, string numero, 
            string complemento, string cep, string bairro, string cidade, string estado, string uf, string tipoEndereco);
        Task<List<Fornecedor>> ListarFornecedores();
        Task<List<Fornecedor>> FiltrarPorSetor(string setor);
        Task<Fornecedor> BuscarFornecedor(int id);
        Task<List<TipoEnderecoResponse>> ObterTipoEndereco();
        Task<List<TipoTelefoneResponse>> ObterTipoTelefone();
    }
}
