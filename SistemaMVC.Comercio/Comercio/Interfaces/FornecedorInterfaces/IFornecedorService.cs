using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorService
    {
        Task<List<Fornecedor>> ListarFornecedores();
        Task<List<Fornecedor>> FiltrarPorSetor(string setor);
    }
}
