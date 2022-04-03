using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.SetorInterfaces
{
    public interface ISetorService
    {
        Task<Setor> Inserir(string descricao);
        Task<IEnumerable<Setor>> ListarSetores();
        Task<Setor> ObterSetor(int id);
        Task<Setor> AtualizarSetor(SetorViewModel setor);
        Task<bool> ExcluirSetor(int id);
    }
}
