using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome_empresa { get; set; }
        public string Telefone_empresa { get; set; }
        public string Nome_vendedor{ get; set; }
        public string Telefone_vendedor { get; set; }
        public sbyte Ativo { get; set; }
        public DateTime Data_criacao { get; set; }
        public DateTime Data_alteracao { get; set; }
    }
}
