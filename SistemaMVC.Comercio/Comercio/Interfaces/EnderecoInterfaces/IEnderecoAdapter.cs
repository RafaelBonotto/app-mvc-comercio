using Comercio.Entities;

namespace Comercio.Interfaces.EnderecoInterfaces
{
    public interface IEnderecoAdapter
    {
        EnderecoFornecedor MontaInsertEnderecoFornecedor(int fornecedorId, int enderecoId);
    }
}
