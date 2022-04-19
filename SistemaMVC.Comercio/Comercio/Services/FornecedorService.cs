using Comercio.Entities;
using Comercio.Interfaces.FornecedorInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class FornecedorService : IFornecedorService
    {
        public Task<List<Fornecedor>> FiltrarPorSetor(string setor)
        {
            throw new System.NotImplementedException();
        }

        public Task<Fornecedor> InserirFornecedor()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Fornecedor>> ListarFornecedores()
        {
            throw new System.NotImplementedException();
        }
    }
}
