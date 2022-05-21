using Comercio.Data.ConnectionManager;
using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using System.Collections.Generic;

namespace Comercio.Data.Repositories.Telefones
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly IMySqlConnectionManager _connection;

        public TelefoneRepository(IMySqlConnectionManager connection)
        {
            _connection = connection;
        }

        public bool EditarTelefone(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id)
        {
            throw new System.NotImplementedException();
        }

        public bool InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone)
        {
            throw new System.NotImplementedException();
        }

        public List<string> ListarDescricaoTipoTelefone()
        {
            throw new System.NotImplementedException();
        }

        public List<Telefone> ListarTelefoneFornecedor(int fornecedor_id)
        {
            throw new System.NotImplementedException();
        }
    }
}
