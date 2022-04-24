using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_fornecedor_vendedor")]
    public class FornecedorVendedor
    {
        public int Fornecedor_id { get; set; }
        public int Pessoa_id { get; set; }
    }
}
