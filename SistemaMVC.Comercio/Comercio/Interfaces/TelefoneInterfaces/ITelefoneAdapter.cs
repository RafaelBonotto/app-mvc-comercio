using Comercio.Entities;

namespace Comercio.Interfaces.TelefoneInterfaces
{
    public interface ITelefoneAdapter
    {
        TelefoneFornecedor MontaTelefoneFornecedor(int fornecedorId, int telefoneId);
    }
}
