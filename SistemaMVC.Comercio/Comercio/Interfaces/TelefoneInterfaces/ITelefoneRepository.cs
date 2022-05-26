using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.TelefoneInterfaces
{
    public interface ITelefoneRepository
    {
        Task<bool> AtualizarTelefone(Telefone telefone);
        Task<int> ObterIdTipoTelefone(string tipoTelefone);
        Task<List<TipoTelefoneResponse>> ListarDescricaoTipoTelefone();
        Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone);
        Task<bool> ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id);
        Task<List<Telefone>> ListarTelefoneFornecedor(int fornecedor_id);
        Task<Telefone> GetById(int telefone_id);
    }
}
