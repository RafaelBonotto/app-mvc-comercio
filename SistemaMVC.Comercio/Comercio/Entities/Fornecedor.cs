using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Display(Name = "Fornecedor")]
        public string Nome_empresa { get; set; }

        [Display(Name = "Telefone")]
        public List<Telefone> Telefones { get; set; }

        [Display(Name = "Vendedor")]
        public List<Pessoa> Vendedores { get; set; }

        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
