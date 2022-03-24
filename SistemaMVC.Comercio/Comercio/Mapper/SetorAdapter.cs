using Comercio.Entities;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Mapper
{
    public class SetorAdapter : ISetorAdapter
    {
        public IEnumerable<SetorViewModel> MontaSetorViewModel(IEnumerable<Setor> setoresBanco)
        {
            List<SetorViewModel> ret = new();
            foreach (var setor in setoresBanco)
                ret.Add(new SetorViewModel()
                {
                    Id = setor.Id,
                    Descricao = setor.Descricao.ToUpper()
                });
            return ret;
        }
    }
}
