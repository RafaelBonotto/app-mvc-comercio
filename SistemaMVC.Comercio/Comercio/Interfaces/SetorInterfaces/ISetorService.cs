using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.SetorInterfaces
{
    public interface ISetorService
    {
        Task<IEnumerable<Setor>> ListarSetores();
        Task<Setor> ObterSetor(int id);
        Task<Setor> AtualizarSetor(Setor setor);
    }
}
