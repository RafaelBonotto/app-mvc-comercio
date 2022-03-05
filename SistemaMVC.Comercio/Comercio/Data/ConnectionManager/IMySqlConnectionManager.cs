using MySqlConnector;
using System.Threading.Tasks;

namespace Comercio.Data.ConnectionManager
{
    public interface IMySqlConnectionManager
    {
        Task<MySqlConnection> GetConnectionAsync();
    }
}
