using Comercio.Enums;
using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_telefone")]
    public class Telefone
    {
        public int Id { get; set; }
        public int Tipo_telefone_id { get; set; }
        public string Tipo_telefone { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
