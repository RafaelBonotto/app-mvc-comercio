using Comercio.Entities;
using Comercio.Interfaces.FornecedorInterfaces;
using Comercio.Models;

namespace Comercio.Mapper
{
    public class FornecedorAdapter : IFornecedorAdapter
    {
        public FornecedorViewModel CriarFornecedorViewModel(Fornecedor fornecedor)
        {
            throw new System.NotImplementedException();
        }

        public FornecedorEndereco MontaFornecedorEndereco(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }

        public Fornecedor MontaFornecedorInsertRepositorio(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }

        public FornecedorTelefone MontaFornecedorTelefone(FornecedorViewModel fornecedor)
        {
            throw new System.NotImplementedException();
        }
    }
}
