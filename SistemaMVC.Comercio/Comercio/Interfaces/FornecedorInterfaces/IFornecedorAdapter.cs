using Comercio.Entities;
using Comercio.Models;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
    }
}
