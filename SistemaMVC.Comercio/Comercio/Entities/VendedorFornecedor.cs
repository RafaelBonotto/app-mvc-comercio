using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_vendedor_fornecedor")]
    public class VendedorFornecedor
    {
        public int Fornecedor_id { get; set; }
        public int Pessoa_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

    }
}
