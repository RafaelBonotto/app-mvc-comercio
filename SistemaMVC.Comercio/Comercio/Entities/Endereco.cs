using Dapper.Contrib.Extensions;
using System;

namespace Comercio.Entities
{
    [Table("tb_endereco")]
    public class Endereco
    {
        public int Id { get; set; }
        public int Tipo_endereco_id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string UF { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public string Tipo_endereco { get; set; }
    }
}
