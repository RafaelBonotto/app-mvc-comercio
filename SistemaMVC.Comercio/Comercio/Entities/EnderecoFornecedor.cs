using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_endereco_fornecedor")]
    public class EnderecoFornecedor
    {
        public int Fornecedor_id { get; set; }
        public int Endereco_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public virtual Endereco Endereco { get; set; }

        [Write(false)]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
