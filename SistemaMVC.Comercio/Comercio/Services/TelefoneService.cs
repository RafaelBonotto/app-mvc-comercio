using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using System;
using System.Collections.Generic;

namespace Comercio.Services
{
    public class TelefoneService : ITelefoneService
    {
        public bool EditarTelefone(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirTelefoneFornecedor(int fornecedor_id, int telefone_id)
        {
            throw new NotImplementedException();
        }

        public bool InserirTelefoneFornecedor(int fornecedor_id, Telefone telefone)
        {
            throw new NotImplementedException();
        }

        public List<string> ListarDescricaoTipoTelefone()
        {
            throw new NotImplementedException();
        }

        public List<Telefone> ListarTelefoneFornecedor(int fornecedor_id)
        {
            throw new NotImplementedException();
        }
    }
}
