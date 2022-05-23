using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task InserirVendedor(int fornecedor_id, Vendedor vendedor);
    }
}
