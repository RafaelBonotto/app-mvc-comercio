using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.SetorInterfaces
{
    public interface ISetorAdapter
    {
        IEnumerable<SetorViewModel> MontaSetorViewModel(IEnumerable<Setor> setoresBanco);
    }
}
