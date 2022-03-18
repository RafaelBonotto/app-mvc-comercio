using Comercio.Entities;
using Comercio.Models;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoAdapter
    {
        Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio);
        Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel);
        ProdutoViewModel MontaProdutoViewModel(Produto produto);
    }
}
