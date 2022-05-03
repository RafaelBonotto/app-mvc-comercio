using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_telefone_fornecedor")]
    public class TelefoneFornecedor
    {       
        public int Fornecedor_id { get; set; }
        public int Telefone_id { get; set; }

        [Write(false)]
        public virtual Telefone Telefone { get; set; }

        [Write(false)]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
