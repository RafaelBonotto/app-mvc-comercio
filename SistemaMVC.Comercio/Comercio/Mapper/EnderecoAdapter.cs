using Comercio.Entities;
using Comercio.Interfaces.EnderecoInterfaces;
using System;

namespace Comercio.Mapper
{
    public class EnderecoAdapter : IEnderecoAdapter
    {
        public EnderecoFornecedor MontaInsertEnderecoFornecedor(int fornecedorId, int enderecoId)
        {
            return new EnderecoFornecedor()
            {
                Fornecedor_id = fornecedorId,
                Endereco_id = enderecoId,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }
    }
}
