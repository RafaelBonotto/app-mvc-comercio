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
        private readonly IFornecedorRepository _repository;
        private readonly IFornecedorAdapter _mapper;

        public FornecedorService(IRepositoryBase<Fornecedor> repository, IFornecedorRepository fornecedorRepository, IFornecedorAdapter mapper)
        {
            _repositoryBase = repository;
            _repository = fornecedorRepository;
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
                var fornecedorResponse =  await _repositoryBase.AddAsync(fornecedorRepository);       
                return fornecedorResponse;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Fornecedor> InserirTelefone(int fornecedor_id, Telefone telefone)
        {
            try
            {
                await _repository.InserirTelefone(fornecedor_id, telefone);
                return await this.BuscarFornecedor(fornecedor_id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Fornecedor> InserirEndereco(int fornecedor_id, Endereco endereco)
        {
            try
            {
                await _repository.InserirEndereco(fornecedor_id, endereco);
                return await this.BuscarFornecedor(fornecedor_id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Fornecedor>> ListarFornecedores()
        {
            try
            {
                return await _repositoryBase.GetAllAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Fornecedor> BuscarFornecedor(int id)
        {
            try
            {
                return await _repositoryBase.GetByIdAsync(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
