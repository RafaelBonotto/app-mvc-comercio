using System;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Display(Name = "Fornecedor")]
        public string Nome_empresa { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(12)]
        public string Telefone_empresa { get; set; }

        [Display(Name = "Vendedor")]
        public string Nome_vendedor { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(12)]
        public string Telefone_vendedor { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
