using Comercio.Entities;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using System;
using System.Collections.Generic;
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
