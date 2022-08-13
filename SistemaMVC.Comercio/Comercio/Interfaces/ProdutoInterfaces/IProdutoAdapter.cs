using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.ProdutoInterfaces
{
    public interface IProdutoAdapter
    {
        Produto MontaProdutoUpdateRepositorio(ProdutoViewModel produtoViewModel);
        Produto MontaProdutoInsertRepositorio(ProdutoViewModel produtoViewModel);
        ProdutoViewModel MontaProdutoViewModel(Produto produto);
        List<FornecedorViewModel> MontaListaFornecedorViewModel(List<Fornecedor> fornecedores);
        ObterFornecedorDetalhesViewModel CriarObterFornecedorDetalhesViewModel(Fornecedor fornecedor, int produto_id);
    }
}
