using Comercio.Data.Repositories.Response;
using Comercio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.FornecedorInterfaces
{
    public interface IFornecedorRepository
    {
        Task<bool> InserirVendedor(int fornecedor_id, PessoaContato vendedor, List<Telefone> telefones);
    }
}
