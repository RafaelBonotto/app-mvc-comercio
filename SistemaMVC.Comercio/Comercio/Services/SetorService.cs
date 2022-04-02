using Comercio.Entities;
using Comercio.Interfaces.Base;
using Comercio.Interfaces.SetorInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class SetorService : ISetorService
    {
        private readonly IRepositoryBase<Setor> _repositoryBase;

        public SetorService(IRepositoryBase<Setor> repository)
        {
            _repositoryBase = repository;
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

        public async Task<Setor> AtualizarSetor(Setor setor)
        {
            try
            {
                var setorBanco = await _repositoryBase.GetByIdAsync(setor.Id);
                setorBanco.Descricao = setor.Descricao;
                return await _repositoryBase.UpdateAsync(setorBanco);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
