using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> FiltrarPorDescricao(string descricao);
        Task<List<Produto>> FiltrarPorSetor(string setor);
        Task<List<Setor>> ObterSetores();
        Task<int> ObterSetorId(string setor);
    }
}
