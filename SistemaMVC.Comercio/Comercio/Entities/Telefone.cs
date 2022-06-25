using Comercio.Data.Repositories.Response;
using Comercio.Enums;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Entities
{
    [Table("tb_telefone")]
    public class Telefone
    {
        public int Id { get; set; }
        public int Tipo_telefone_id { get; set; }

        [Write(false)]
        [Display(Name = "TIPO")]
        public string Tipo_telefone { get; set; }

        [Display(Name = "DDD")]
        public string Ddd { get; set; }

        [Display(Name = "NÚMERO")]
        public string Numero { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; } 
        public DateTime Data_alteracao { get; set; }

        [Write(false)]
        public List<TipoTelefoneResponse> TiposTelefone { get; set; }
    }
}
