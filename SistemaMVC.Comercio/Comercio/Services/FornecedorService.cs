using Comercio.Entities;
using Comercio.Exceptions.Fornecedor;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Fornecedor> InserirFornecedor(FornecedorViewModel fornecedor)
        {
            try
            {
                if (string.IsNullOrEmpty(fornecedor.Cnpj))
                {
                    var checkFornecedor = await _repositoryBase.GetByKeyAsync(fornecedor.Cnpj);
                    if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 1)
                        throw new CnpjInvalidoException();
                    if (checkFornecedor.Any() && checkFornecedor.First().Ativo == 0)
                    {
                        fornecedor.Id = checkFornecedor.First().Id;
                        //return await this.AtualizarFornecedor(fornecedor);
                    }
                }                
                var fornecedorRepository = _mapper.MontaFornecedorInsertRepositorio(fornecedor);
                return await _repositoryBase.AddAsync(fornecedorRepository);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<List<Fornecedor>> ListarFornecedores()
        {
            throw new System.NotImplementedException();
        }
    }
}
