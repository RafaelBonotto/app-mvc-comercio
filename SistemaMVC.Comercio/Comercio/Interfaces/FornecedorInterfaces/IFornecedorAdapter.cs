using Comercio.Entities;
using Comercio.Models;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
    }
}
