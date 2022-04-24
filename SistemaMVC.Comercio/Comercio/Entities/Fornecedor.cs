using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Comercio.Entities
{
    [Table("tb_fornecedor")]
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome_empresa { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public virtual List<Produto> Produtos { get; set; }
         
        [Write(false)]
        public virtual List<Endereco> Endereco { get; set; }
        
        [Write(false)]
        public virtual List<Telefone> Telefone { get; set; }
        
        [Write(false)]
        public virtual List<Vendedor> Vendedor { get; set; }
    }
}
