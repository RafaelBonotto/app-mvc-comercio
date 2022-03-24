using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.SetorInterfaces
{
    public interface ISetorRepository
    {
        Task<IEnumerable<Setor>> ListarSetores();
    }
}
