using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.SetorInterfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Setores
{
    public class SetorRepository : ISetorRepository
    {
        private readonly IMySqlConnectionManager _connection;

        public SetorRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Setor>> ListarSetores()
        {
            var sql = "SELECT * FROM tb_setor WHERE tb_setor.ativo = 1";
            using var connection = await _connection.GetConnectionAsync();
            return await connection.QueryAsync<Setor>(sql);
        }
    }
}
