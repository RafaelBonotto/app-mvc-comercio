using Comercio.Entities;
using Comercio.Interfaces.TelefoneInterfaces;
using System;

namespace Comercio.Mapper
{
    public class TelefoneAdapter : ITelefoneAdapter
    {
        public TelefoneFornecedor MontaTelefoneFornecedor(int fornecedorId, int telefoneId)
        {
            return new TelefoneFornecedor()
            {
                Fornecedor_id = fornecedorId,
                Telefone_id = telefoneId,
                Ativo = 1,
                Data_criacao = DateTime.Now,
                Data_alteracao = DateTime.Now
            };
        }
    }
}
