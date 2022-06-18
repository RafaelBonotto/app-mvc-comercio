using Comercio.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class FornecedorViewModel 
    {
        public int Id { get; set; }

        [Display(Name = "Cnpj")]
        public string Cnpj { get; set; }

        [Display(Name = "Fornecedor")]
        public string Nome_empresa { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inaválido")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        public List<Endereco> Endereco { get; set; } = new();

        [Display(Name = "Telefone")]
        public List<Telefone> Telefone { get; set; } = new();

        [Display(Name = "Vendedor")]
        public List<PessoaContato> Vendedor { get; set; } = new();

        public IEnumerable<SelectListItem> TipoEndereco { get; set; }

        public IEnumerable<SelectListItem> TipoTelefone { get; set; }
    }
}
