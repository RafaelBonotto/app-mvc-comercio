using Comercio.Entities;
using System.Collections.Generic;

namespace Comercio.Interfaces.TelefoneInterfaces
{
    public interface ITelefoneRepository
    {
        bool EditarTelefone(int id);
        List<string> ListarDescricaoTipoTelefone();
        bool InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone);
        bool ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id);
        List<Telefone> ListarTelefoneFornecedor(int fornecedor_id);
    }
}
