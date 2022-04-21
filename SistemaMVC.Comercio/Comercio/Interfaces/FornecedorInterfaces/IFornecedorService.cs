using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorService
    {
        Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor);
        Task<List<Fornecedor>> ListarFornecedores();
        Task<List<Fornecedor>> FiltrarPorSetor(string setor);
    }
}
