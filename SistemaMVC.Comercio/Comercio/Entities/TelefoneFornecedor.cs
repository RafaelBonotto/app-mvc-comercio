using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_telefone_fornecedor")]
    public class TelefoneFornecedor
    {       
        public int Fornecedor_id { get; set; }
        public int Telefone_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
        [Write(false)]
        public virtual Telefone Telefone { get; set; }

        [Write(false)]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
