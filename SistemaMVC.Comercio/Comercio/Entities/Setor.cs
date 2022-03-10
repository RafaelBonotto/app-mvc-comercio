using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comercio.Entities
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
