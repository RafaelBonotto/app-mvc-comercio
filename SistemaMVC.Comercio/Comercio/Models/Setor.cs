using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Models
{
    [Table("tb_setor")]
    public class Setor
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
