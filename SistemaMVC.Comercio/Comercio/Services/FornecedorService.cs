using Comercio.Entities;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.FornecedorInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IRepositoryBase<Fornecedor> _repositoryBase;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorService(IRepositoryBase<Fornecedor> repository, IFornecedorAdapter mapper)
        {
            _repositoryBase = repository;
            _mapper = mapper;
        }

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
