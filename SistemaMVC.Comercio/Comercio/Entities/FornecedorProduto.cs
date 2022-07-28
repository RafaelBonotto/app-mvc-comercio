using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_fornecedor_produto")]
    public class FornecedorProduto
    {
        public int Id { get; set; }
        public int Fornecedor_id { get; set; }
        public int Produto_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}