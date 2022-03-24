using Comercio.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Models
{
    public class SetorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "SETOR")]
        [MaxLength(50)]
        [Required(ErrorMessage = ("O campo descrição é obrigatório"), AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Display(Name = "STATUS")]
        public string Ativo { get; set; }

        public virtual List<Produto> Produtos { get; set; }
    }
}
