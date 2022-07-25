using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoAdapter
    {
        Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel, Produto produtoRepositorio);
        Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel);
        ProdutoViewModel MontaProdutoViewModel(Produto produto);
        List<FornecedorViewModel> MontaListaFornecedorViewModel(List<Fornecedor> fornecedores);
        ListarFornecedorViewModel CriarListaFornecedorViewModel(Fornecedor fornecedor, int produto_id);
    }
}
