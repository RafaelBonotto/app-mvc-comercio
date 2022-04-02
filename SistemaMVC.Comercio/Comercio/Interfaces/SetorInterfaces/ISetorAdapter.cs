using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.SetorInterfaces
{
    public interface ISetorAdapter
    {
        IEnumerable<SetorViewModel> MontaListaSetorViewModel(IEnumerable<Setor> setoresBanco);
        SetorViewModel MontaSetorViewModel(Setor setor);
        Setor MontaSetor(SetorViewModel setorViewModel);
    }
}
