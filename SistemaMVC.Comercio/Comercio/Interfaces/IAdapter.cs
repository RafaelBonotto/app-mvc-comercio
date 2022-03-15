using Comercio.Entities;
using Comercio.Models;

namespace Comercio.Interfaces
{
    public interface IAdapter
    {
        Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio);
        Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel);
        ProdutoViewModel MontaProdutoViewModel(Produto produto);
    }
}
