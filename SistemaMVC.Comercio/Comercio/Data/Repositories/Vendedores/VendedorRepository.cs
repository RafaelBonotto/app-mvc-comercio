using Comercio.Data.ConnectionManager;
using Comercio.Interfaces.VendedorInterfaces;

namespace Comercio.Data.Repositories.Vendedores
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly IMySqlConnectionManager _connection;
        private readonly IVendedorAdapter _mapperVendedor;

        public VendedorRepository(
            IMySqlConnectionManager connection, 
            IVendedorAdapter mapperVendedor)
        {
            _connection = connection;
            _mapperVendedor = mapperVendedor;
        }
    }
}
