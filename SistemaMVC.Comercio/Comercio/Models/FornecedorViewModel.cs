using Comercio.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class FornecedorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fornecedor")]
        public string Nome_empresa { get; set; }

        [Display(Name = "Telefone")]
        public List<Telefone> Telefones { get; set; }

        [Display(Name = "Vendedor")]
        public List<Pessoa> Vendedores { get; set; }
    }
}
