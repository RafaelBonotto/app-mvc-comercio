using Comercio.Entities;
using Comercio.Exceptions.Setor;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class SetorService : ISetorService
    {
        private readonly IRepositoryBase<Setor> _repositoryBase;
        private readonly ISetorAdapter _mapper;

        public SetorService(IRepositoryBase<Setor> repository, ISetorAdapter mapper)
        {
            _repositoryBase = repository;
            _mapper = mapper;
        }

        public async Task<Setor> Inserir(string descricao)
        {
            try
            {
                var checkSetor = await _repositoryBase.GetByKeyAsync(descricao);  
                if (checkSetor.Any() && checkSetor.First().Ativo == 1)
                    throw new DescricaoInvalidaException();
                if (checkSetor.Any() && checkSetor.First().Ativo == 0)
                {
                    checkSetor.First().Ativo = 1;
                    checkSetor.First().Data_alteracao = DateTime.Now;
                    return await _repositoryBase.UpdateAsync(checkSetor.First());
                }
                var setorRepository = _mapper.MontaInsertSetor(descricao);
                return await _repositoryBase.AddAsync(setorRepository);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Setor>> ListarSetores()
        {
            try
            {
                return await _repositoryBase.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Setor> ObterSetor(int id)
        {
            try
            {
                return await _repositoryBase.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Setor> AtualizarSetor(SetorViewModel setor)
        {
            try
            {
                var setorBanco = await _repositoryBase.GetByIdAsync(setor.Id);
                setorBanco.Descricao = setor.Descricao.ToUpper();
                setorBanco.Data_alteracao = DateTime.Now;
                return await _repositoryBase.UpdateAsync(setorBanco);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExcluirSetor(int id)
        {
            try
            {
                Setor response = await _repositoryBase.DeleteAsync(id);
                if (response is null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
