using Comercio.Entities;
using Comercio.Interfaces.SetorInterfaces;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Mapper
{
    public class SetorAdapter : ISetorAdapter
    {
        public IEnumerable<SetorViewModel> MontaListaSetorViewModel(IEnumerable<Setor> setoresBanco)
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
        public SetorViewModel MontaSetorViewModel(Setor setor)
        {
            return new SetorViewModel()
            {
                Id = setor.Id,
                Descricao = setor.Descricao.ToUpper()
            };
        }

        public Setor MontaSetor(SetorViewModel setorViewModel)
        {
            return new Setor()
            {
                Id = setorViewModel.Id,
                Descricao = setorViewModel.Descricao.ToUpper()
            };
        }

    }
}
