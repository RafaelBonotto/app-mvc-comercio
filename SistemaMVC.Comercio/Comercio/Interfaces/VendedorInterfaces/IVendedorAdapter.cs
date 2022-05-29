using Comercio.Entities;

namespace Comercio.Interfaces.VendedorInterfaces
{
    public interface IVendedorAdapter
    {
        VendedorFornecedor MontaVendedorFornecedor(int fornecedor_id, int pessoa_id);
    }
}
