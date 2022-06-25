using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using Comercio.Requests.Fornecedor;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.TelefoneInterfaces
{
    public interface ITelefoneRepository
    {
        Task<bool> AtualizarTelefone(TelefoneRequest telefone, MySqlConnection connection = null);
        Task<int> ObterIdTipoTelefone(string tipoTelefone, MySqlConnection conn = null);
        Task<List<TipoTelefoneResponse>> ListarDescricaoTipoTelefone(MySqlConnection connection = null);
        Task<bool> InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone, MySqlConnection connection = null);
        Task<bool> ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id);
        Task<List<Telefone>> ListarTelefoneFornecedor(int fornecedor_id, MySqlConnection conn = null);
        Task<Telefone> GetById(int telefone_id);
    }
}
