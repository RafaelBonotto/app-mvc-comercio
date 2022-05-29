using Comercio.Entities;
using System.Threading.Tasks;

namespace Comercio.Interfaces.VendedorInterfaces
{
    public interface IVendedorRepository
    {
        Task<bool> InserirVendedorFornecedor(int fornecedor_id, Pessoa pessoa);
    }
}
