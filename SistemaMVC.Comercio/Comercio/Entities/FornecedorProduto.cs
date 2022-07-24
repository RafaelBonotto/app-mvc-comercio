using Dapper.Contrib.Extensions;

namespace Comercio.Entities
{
    [Table("tb_fornecedor_produto")]
    public class FornecedorProduto
    {
        public int Id { get; set; }
        public int Fornecedor_id { get; set; }
        public int Produto_id { get; set; }
    }
}