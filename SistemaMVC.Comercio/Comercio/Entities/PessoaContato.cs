using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Comercio.Entities
{
    [Table("tb_pessoa_contato")]
    public class PessoaContato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public virtual List<Telefone> Telefones { get; set; }
    }
}
