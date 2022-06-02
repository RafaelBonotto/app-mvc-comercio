using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_pessoa_contato_telefone")]
    public class PessoaContatoTelefone
    {
        public int Pessoa_contato_id { get; set; }
        public int Telefone_id { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; } 
    }
}
