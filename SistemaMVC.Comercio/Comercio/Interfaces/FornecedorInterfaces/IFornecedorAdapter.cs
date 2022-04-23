using Comercio.Entities;
using Comercio.Models;
using System.Collections.Generic;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorAdapter
    {
        Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor);
        //List<FornecedorEndereco> MontaFornecedorEndereco(FornecedorViewModel fornecedor);
        //List<FornecedorTelefone> MontaFornecedorTelefone(FornecedorViewModel fornecedor);
        //List<FornecedorVendedor> MontaVendedorFornecedor(FornecedorViewModel fornecedor);
        FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor);
    }
}
