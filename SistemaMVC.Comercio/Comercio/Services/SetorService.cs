using Comercio.Entities;
using Comercio.Interfaces.SetorInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;

        public SetorService(ISetorRepository repository)
        {
            _setorRepository = repository;
        }
        public async Task<IEnumerable<Setor>> ListarSetores()
        {
            try
            {
                return await _setorRepository.ListarSetores();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
