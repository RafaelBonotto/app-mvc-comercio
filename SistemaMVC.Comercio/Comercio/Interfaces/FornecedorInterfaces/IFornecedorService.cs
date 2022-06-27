using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Models;
using Comercio.Requests.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorService
    {
        Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor);
        Task<bool> InserirVendedor(VendedorRequest request);
        Task<Fornecedor> InserirTelefone(TelefoneRequest request);
        Task<Fornecedor> EditarTelefone(TelefoneRequest request);
        Task<Fornecedor> ExcluirTelefone(int fornecedor_id, int telefone_id);
        Task<Fornecedor> InserirEndereco(EnderecoRequest request);
        Task<bool> EditarEndereco(EnderecoRequest endereco);
        Task<bool> EditarVendedor(VendedorRequest request);
        Task<bool> ExcluirVendedor(int fornecedor_id, int vendedor_id);
        Task<bool> ExcluirEndereco(int fornecedor_id, int endereco_id);
        Task<Fornecedor> EditarNomeEmail(int fornecedor_id, string nome, string email);
        Task<List<Fornecedor>> ListarFornecedores();
        Task<List<Fornecedor>> FiltrarPorSetor(string setor);
        Task<Fornecedor> BuscarFornecedor(int id);
        Task<FornecedorViewModel> RetornarForncedorViewModel(int fornecedor_id);
        Task<TelefoneFornecedorViewModel> RetornarTelefoneFornecedorViewModel(int fornecedor_id, int telefone_id);
        Task<EnderecoFornecedorViewModel> RetornarEnderecoFornecedorViewModel(int fornecedor_id, int endreco_id);
        Task<VendedorFornecedorViewModel> RetornarVendedorFornecedorViewModel(int fornecedor_id, int vendedor_id);
    }
}
