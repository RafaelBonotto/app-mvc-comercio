using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_pessoa_contato_fornecedor")]
    public class PessoaContatoFornecedor
    {
        public int Fornecedor_id { get; set; }
        public int PessoaContato_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}