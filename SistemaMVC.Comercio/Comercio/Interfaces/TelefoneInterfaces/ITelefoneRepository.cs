using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.TelefoneInterfaces
{
    public interface ITelefoneRepository
    {
        Task<bool> AtualizarTelefone(Telefone telefone);
        Task<int> ObterIdTipoTelefone(string tipoTelefone);
        Task<List<TipoTelefoneResponse>> ListarDescricaoTipoTelefone(MySqlConnection connection = null);
        Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone, MySqlConnection connection = null);
        Task<bool> ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id);
        Task<List<Telefone>> ListarTelefoneFornecedor(int fornecedor_id, MySqlConnection conn = null);
        Task<Telefone> GetById(int telefone_id);
    }
}
