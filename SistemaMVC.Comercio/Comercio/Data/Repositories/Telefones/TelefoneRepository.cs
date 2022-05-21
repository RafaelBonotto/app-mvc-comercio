using Comercio.Data.ConnectionManager;
using Comercio.Interfaces.TelefoneInterfaces;

namespace Comercio.Data.Repositories.Telefones
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly IMySqlConnectionManager _connection;

        public TelefoneRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }
    }
}
