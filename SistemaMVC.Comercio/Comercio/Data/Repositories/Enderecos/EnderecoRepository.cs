using Comercio.Data.ConnectionManager;
using Comercio.Interfaces.EnderecoInterfaces;

namespace Comercio.Data.Repositories.Enderecos
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IMySqlConnectionManager _connection;

        public EnderecoRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }
    }
}
