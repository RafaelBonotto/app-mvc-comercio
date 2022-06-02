using Comercio.Entities;
using Comercio.Interfaces.VendedorInterfaces;
using System;

namespace Comercio.Mapper
{
    public class VendedorAdapter : IVendedorAdapter
    {
        public VendedorFornecedor MontaVendedorFornecedor(int fornecedor_id, int pessoa_id)
        {
            return new VendedorFornecedor
            {
                Fornecedor_id = fornecedor_id,
                Pessoa_id = pessoa_id,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }
    }
}
