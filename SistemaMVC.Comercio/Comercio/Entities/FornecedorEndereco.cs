using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_fornecedor_endereco")]
    public class FornecedorEndereco
    {
        public int Fornecedor_id { get; set; }
        public int Endereco_id { get; set; }

        [Write(false)]
        public virtual Endereco Endereco { get; set; }

        [Write(false)]
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
