using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_fornecedor_telefone")]
    public class FornecedorTelefone
    {       
        public int Fornecedor_id { get; set; }
        public int Telefone_id { get; set; }

        [Write(false)]
        public virtual Telefone Telefone { get; set; }

        [Write(false)]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
